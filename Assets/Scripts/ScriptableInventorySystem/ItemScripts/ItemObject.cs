using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType //specify possible item types
{
    Consumable,
    Sword,
    Earring,
    Necklace,
    Material,
    Key
}

public enum ItemAttribute
{
    SellingValue,
    RestorationValue,
    ItemStage,
    BaseDEF,
    BaseHP,
    BaseATK,
    BaseAP,
    BaseCRIT,
    BaseCRITDMG
}

public abstract class ItemObject : ScriptableObject
{
    public int Id;
    public Sprite uiDisplay; //to hold the display for the object
    public ItemType type;
    public bool stackable;
    [TextArea(15, 20)] public string description;

    public ItemBuff[] buffs;

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}


[System.Serializable]
public class Item
{
    public string name;
    public int ID;
    public bool stackable;
    public string itemType;
    public ItemBuff[] buffs;
    
    //default constructor
    public Item()
    {
        name = "";
        ID = -1;
    }

    public Item(ItemObject item)
    {
        name = item.name;
        ID = item.Id;
        stackable = item.stackable;
        itemType = item.type.ToString();
        buffs = new ItemBuff[item.buffs.Length];
        
        //copying buffs over
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.buffs[i].min, item.buffs[i].max);
            {
                buffs[i].attribute = item.buffs[i].attribute;
            }
        }
    }
}

[System.Serializable]
public class ItemBuff
{
    public ItemAttribute attribute;
    public int value;
    public int min;
    public int max;

    public ItemBuff(int _min, int _max)
    {
        min = _min;
        max = _max;
        GenerateValue();
    }

    public void GenerateValue()
    {
        value = UnityEngine.Random.Range(min, max);
    }
}