using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour
{

    public List<DropCondition> dropConditions = new List<DropCondition>();
    public event Action<DraggableComponent> onDropHandler;

    public bool Accepts(DraggableComponent draggable)
    {
        return dropConditions.TrueForAll(cond => cond.check(draggable));
    }

    public void Drop(DraggableComponent draggable)
    {
        onDropHandler?.Invoke(draggable);
    }

}
