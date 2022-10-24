using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsConsumable : DropCondition
{
    public override bool check(DraggableComponent draggable)
    {
        var item = draggable.GetComponent<ItemSlot>().item;
        return item == null 
            ? false 
            : item is Consumable ? true : false;
    }
}
