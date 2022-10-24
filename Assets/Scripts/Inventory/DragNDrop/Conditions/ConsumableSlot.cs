using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableSlot : ItemSlotManager
{
    protected override void Awake()
    {
        base.Awake();

        dropArea.dropConditions.Add(new IsConsumable());
    }
}
