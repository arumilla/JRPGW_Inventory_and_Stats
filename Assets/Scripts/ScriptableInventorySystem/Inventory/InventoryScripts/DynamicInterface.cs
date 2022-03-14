using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicInterface : UserInterface
{
    public GameObject inventoryPrefab;
    
    // starting position of inventory slots
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int Y_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public override void CreateSlots()
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
    
    private Vector3 GetPosition(int i)
    {
        return new Vector3(
            (X_START + X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN))
            ,(Y_START + (-Y_SPACE_BETWEEN_ITEM*(i/NUMBER_OF_COLUMN)))
            ,0f);
    }
}
