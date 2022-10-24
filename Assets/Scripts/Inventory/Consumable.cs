using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Consumable", order = 1)]
public class Consumable : StackableItem
{

    public ConsumableEffect[] effects;

    public Consumable(string name, string description, int id, int qty, int maxStack, ConsumableEffect[] effects)
    {
        this.name = name;
        this.description = description;
        this.id = id;
        this.qty = qty;
        this.maxStack = maxStack;
        this.effects = effects;
    }
}
