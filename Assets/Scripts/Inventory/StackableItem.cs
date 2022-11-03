using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StackableItem : Item
{

    public int qty;
    public int maxStack;

    protected StackableItem(string name, string description, int id, Sprite sprite, int qty, int maxStack):base(name, description, id, sprite)
    {
        this.qty = qty;
        this.maxStack = maxStack;
    }

    public override string GetDisplayMessage()
    {
        return this.name + (qty > 1 ? ": " + qty : "");
    }

}
