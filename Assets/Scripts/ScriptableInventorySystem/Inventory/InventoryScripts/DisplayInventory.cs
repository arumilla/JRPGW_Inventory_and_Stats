using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DisplayInventory : MonoBehaviour
{
    public UserInterface.MouseItem mouseItem = new UserInterface.MouseItem();
    
    public GameObject inventoryPrefab;
    public InventoryObject inventory; // note: inventory contains the inventory slots

    // starting position of inventory slots
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int Y_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    
    Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
    void Start()
    {
        CreateSlots();
    }
    void Update()
    {
        UpdateSlots();
    }
    
    public void UpdateSlots()
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in itemsDisplayed)
        {
            if (_slot.Value.ID >= 0)
            {
                //setting the image
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite 
                    = inventory.database.GetItem[_slot.Value.item.ID].uiDisplay;
                
                //setting the image to make sure that it displays (the original prefab had it so
                //not having any colors made it invisible (alpha = 0)
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color 
                    = new Color(1, 1, 1, 1);
                
                //updating the text
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text 
                    //if there is only 1 of something in the inventory, then the number doesnt show up
                    = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
                // *note: "n0" makes it so that it automatically adds commas to a number (if it gets high enough)
            }
            //display nothing if there is nothing
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color 
                    //changing a to 0 makes it zero (like 0% opacity)
                    = new Color(1, 1, 1, 0);
                
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }
    
    private void AddEvent(GameObject objct, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = objct.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }

    public void CreateSlots()
    {
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.container.Items.Length; i++)
        {
            var objct = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            objct.GetComponent<RectTransform>().localPosition = GetPosition(i);

            AddEvent(objct, EventTriggerType.PointerEnter, delegate
            {
                OnEnter(objct); 
                
            });
            AddEvent(objct, EventTriggerType.PointerExit, delegate
            {
                OnExit(objct); 
                
            });
            AddEvent(objct, EventTriggerType.BeginDrag, delegate
            {
                OnDragStart(objct); 
                
            });
            AddEvent(objct, EventTriggerType.EndDrag, delegate
            {
                OnDragEnd(objct); 
                
            });
            AddEvent(objct, EventTriggerType.Drag, delegate
            {
                OnDrag(objct); 
                
            });
            
            itemsDisplayed.Add(objct, inventory.container.Items[i]);
        }
    }
    
    //sets the object that the mouse is hovering over to the correct item/makes sure its actually displayed
    public void OnEnter(GameObject selectedObject)
    {
        mouseItem.hoveredObject = selectedObject;
        if (itemsDisplayed.ContainsKey(selectedObject))
            mouseItem.hoveredItem = itemsDisplayed[selectedObject];
    }
    
    //clear mouse when it's not over an item
    public void OnExit(GameObject objct)
    {
        mouseItem.hoveredObject = null;
        mouseItem.hoveredItem = null;
    }
    
    //when mouse starts click-holding
    public void OnDragStart(GameObject objct)
    {
        //empty game object for your mouse - the object selected by the mouse
        var mouseObject = new GameObject();
        
        //making sure the new item is the correct size
        var selectedItemSize = mouseObject.AddComponent<RectTransform>();
        selectedItemSize.sizeDelta = new Vector2(65, 65);
        mouseObject.transform.SetParent(transform.parent);
        
        //checking if theres an item in the item slot that is supposed to be moved 
        if (itemsDisplayed[objct].ID >= 0)
        {
            var img = mouseObject.AddComponent<Image>();
            img.sprite = inventory.database.GetItem[itemsDisplayed[objct].ID].uiDisplay;
            img.raycastTarget = false;
        }
        mouseItem.objct = mouseObject;
        mouseItem.item = itemsDisplayed[objct];
    }
    public void OnDragEnd(GameObject objct)
    {
        //if mouse is hovering over an item
        if (mouseItem.hoveredObject)
        {
            inventory.MoveItem(itemsDisplayed[objct], itemsDisplayed[mouseItem.hoveredObject]);
        }
        else
        {
            inventory.DeleteItem(itemsDisplayed[objct].item);
        }
        Destroy(mouseItem.objct);
        mouseItem.item = null;
    }
    
    //making sure that the item follows the mouse
    public void OnDrag(GameObject objct)
    {
        //if we have an item on the mouse
        if (mouseItem.objct != null)
            mouseItem.objct.GetComponent<RectTransform>().position = Input.mousePosition;
    }
    
    //getting position onscreen
    public Vector3 GetPosition(int i)
    {
        return new Vector3(
             (X_START + X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN))
            ,(Y_START + (-Y_SPACE_BETWEEN_ITEM*(i/NUMBER_OF_COLUMN)))
            ,0f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}

