using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Necklace Object", menuName = "Inventory System/Items/Necklace")]
public class NecklaceEquipmentObject : ItemObject
{
    //public int baseDEF;
    //public int baseHP;
    private void Awake()
    {
        type = ItemType.Necklace;
        stackable = false;
    }
}

