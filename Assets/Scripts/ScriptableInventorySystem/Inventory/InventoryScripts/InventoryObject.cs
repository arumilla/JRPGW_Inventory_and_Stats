using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;


// because of how this inventory is set up, this inventory system can be used to create an inventory for
// anything that needs an inventory (ex: shops, etc.)
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public Inventory container;
    
    // database for all objects
    public ItemDatabaseObject database;


    public void AddItem(Item _item, int _amount)
    {
        //only consumables and materials stack
        if (_item.stackable == false)
        {
            SetEmptySlot(_item, _amount);
            return;
        }
        
        //checking if an item is already in the inventory
        for (int i = 0; i < container.Items.Length; i++)
        {
            if (container.Items[i].ID == _item.ID)
            {
                container.Items[i].AddAmount(_amount);
                return;
            }
        }
        // if not in inventory, this will run to add it to the inventory
        SetEmptySlot(_item, _amount);
        
    }

    public InventorySlot SetEmptySlot(Item _item, int amount)
    {
        for (int i = 0; i < container.Items.Length; i++)
        {
            if (container.Items[i].ID <= -1)
            {
                container.Items[i].UpdateSlot(_item.ID, _item, amount);
                return container.Items[i];
            }   
        }
        //for when inventory full
        return null;
    }
    
    //-----------------------------Interacting with the Inventory---------------------------------------

    public void MoveItem(InventorySlot selectedItem, InventorySlot itemReplaced)
    {
        InventorySlot itemHolder = new InventorySlot(itemReplaced.ID, itemReplaced.item, itemReplaced.amount);
        itemReplaced.UpdateSlot(selectedItem.ID, selectedItem.item, selectedItem.amount);
        selectedItem.UpdateSlot(itemHolder.ID, itemHolder.item, itemHolder.amount);
    }

    public void DeleteItem(Item selectedItem)
    {
        for (int i = 0; i < container.Items.Length; i++)
        {
            if (container.Items[i].item == selectedItem)
            {
                // basically sets it to not an item
                container.Items[i].UpdateSlot(-1, null, 0);
            }
            
        }
    }
    
    // ------------------- Saving and Loading the Inventory -------------------------------------------
    public string savePath;

    [ContextMenu("Save")]
    public void Save()
    {
        //using json makes it easier for players to edit their own save file
        string SaveData = JsonUtility.ToJson(this, true);
        BinaryFormatter format = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        format.Serialize(file, SaveData);
        file.Close();
        Debug.Log("Saved Inventory");
    }

    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
        {
            BinaryFormatter format = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(format.Deserialize(file).ToString(), this);
            file.Close();
            Debug.Log("Loaded Inventory");
        }
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        container.Clear();
    }

}

[System.Serializable]
public class Inventory
{
    //need to hold more than just 1 of each object so inventory will contain list of item slots
    //public List<InventorySlot> Items = new List<InventorySlot>();
    public InventorySlot[] Items = new InventorySlot[12];

    public void Clear()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].UpdateSlot(-1, new Item(),0);
        }
    }
}


[System.Serializable] //so it shows up in the unity editor
public class InventorySlot
{
    public ItemType[] AllowedItems = new ItemType[0];
    public UserInterface parent;
    public Item item;
    public int amount;
    public int ID;
    
    //inventory slot constructor
    public InventorySlot()
    {
        //setting variables to inputted variables
        ID = -1;
        item = null;
        amount = 0;
    }
    
    public InventorySlot(int _id, Item _item, int _amount)
    {
        //setting variables to inputted variables
        ID = _id;
        item = _item;
        amount = _amount;
    }
    
    public void UpdateSlot(int _id, Item _item, int _amount)
    {
        //setting variables to inputted variables
        ID = _id;
        item = _item;
        amount = _amount;
    }
    
    //add to current amount of item
    public void AddAmount(int value)
    {
        amount += value;
    }

    public bool CanPlaceInSlot(ItemObject _item)
    {
        if (AllowedItems.Length <= 0)
        {
            return true;
        }

        for (int i = 0; i < AllowedItems.Length; i++)
        {
            if (_item.type == AllowedItems[i])
            {
                return true;
            }
        }

        return false;
    }
    
}