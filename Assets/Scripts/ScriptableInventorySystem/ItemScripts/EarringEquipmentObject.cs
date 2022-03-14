using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Earring Object", menuName = "Inventory System/Items/Earring")]
public class EarringEquipmentObject : ItemObject
{
    //public int baseAP;
    //public float baseCRIT;
    //public float baseCRITDMG;

    private void Awake()
    {
        type = ItemType.Earring;
        stackable = false;
    }
}

