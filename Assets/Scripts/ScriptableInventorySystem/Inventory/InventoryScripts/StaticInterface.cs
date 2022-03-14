using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StaticInterface : UserInterface
{
    public GameObject[] slots;
    public override void CreateSlots()
    {
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.container.Items.Length; i++)
        {
            var objct = slots[i];
            
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
}
