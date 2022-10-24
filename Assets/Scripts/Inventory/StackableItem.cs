using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StackableItem : Item
{

    public int qty;
    public int maxStack;

    public override string GetDisplayMessage()
    {
        return this.name + (qty > 1 ? ": " + qty : "");
    }

}
