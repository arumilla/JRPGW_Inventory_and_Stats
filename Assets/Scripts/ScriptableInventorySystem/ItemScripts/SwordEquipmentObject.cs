using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sword Object", menuName = "Inventory System/Items/Sword")]
public class SwordEquipmentObject : ItemObject
{
    //public int stage;
    //public int baseATK;
    //public float baseCRIT;
    //public float baseCRITDMG;

    private void Awake()
    {
        type = ItemType.Sword;
        stackable = false;
    }
}
