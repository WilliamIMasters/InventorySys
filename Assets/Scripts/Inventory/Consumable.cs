using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Consumable", order = 1)]
public class Consumable : StackableItem
{

    public ConsumableEffect[] effects;

    public Consumable(string name, string description, int id, Sprite sprite, int qty, int maxStack, ConsumableEffect[] effects) :base(name, description, id, sprite, qty, maxStack)
    {
        this.effects = effects;
    }
}
