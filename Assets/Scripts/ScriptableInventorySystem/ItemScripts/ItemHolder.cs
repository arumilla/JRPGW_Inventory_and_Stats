using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// just so an object can actually hold an item - its also called a Ground Item
public class ItemHolder : MonoBehaviour
{
    public ItemObject item;
    public SpriteRenderer spriteR;

    private void Start()
    {
        spriteR.sprite = item.uiDisplay;
    }
}
