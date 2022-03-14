using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public UserInterface.MouseItem mouseItem = new UserInterface.MouseItem();
    
    bool inventoryOpen = true;
    bool statsSheetOpen = true;
    
    public float moveSpeed;
    public InventoryObject inventory;
    public InventoryObject equipmentInventory;
    
    public GameObject statsSheet;
    public GameObject inventoryUI;
    
    void Update()
    {
        // -------------------------- playerController part for movement ------------------------------------------
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            //moving left or right
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
        }
        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        {
            //moving up or down
            transform.Translate(new Vector3( 0f,Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
        }
        
        // ------------------------------ Saving/Loading ---------------------------
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventory.Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            inventory.Load();
        }
        
        // -------------------------------- Hot Keys for Demo ----------------------------------
        
        
        //opening and closing inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryOpen)
            {
                inventoryUI.SetActive(false);
                inventoryOpen = false;
            }
            else
            {
                inventoryUI.SetActive(true);
                inventoryOpen = true;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (statsSheetOpen)
            {
                statsSheet.SetActive(false);
                statsSheetOpen = false;
            }
            else
            {
                statsSheet.SetActive(true);
                statsSheetOpen = true;
            }
        }
             
    }
    
    // -------------------------- Colliders ------------------------------------------
    void OnTriggerEnter2D(Collider2D other)
    {
        //collecting items
        if (other.gameObject.CompareTag("Item"))
        {
            Debug.Log("contact with item");
            var item = other.GetComponent<ItemHolder>();
            if (item != null)
            {
                inventory.AddItem(new Item(item.item),1);
                Destroy(other.gameObject);
            }
        }
    }
    
    // --------------------------------Inventory specific ------------------------------

    private void OnApplicationQuit()
    {
        // clearing the inventory is done by just remaking the inventory slots
        inventory.container.Items = new InventorySlot[12];
        
        //clearing the equipped items - also done by just remaking the array
        equipmentInventory.container.Items = new InventorySlot[3];
    }
    
    
}

