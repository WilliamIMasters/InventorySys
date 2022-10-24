
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemDataBase 
{

    public static Item GetItemById(int id)
    {
        return allItems.Find(i => i.id == id);
    }

    public static StackableItem GetItemById(int id, int qty)
    {
        var item = GetItemById(id);
        if(item is StackableItem) {
            ((StackableItem)item).qty = qty;
            return (StackableItem)item;
        } else {
            return null;
        }

    }

    private static readonly List<Item> allItems = new List<Item> {
        new Consumable("Apple", "An apple", 0, 1, 16, new ConsumableEffect[0])
    };
}
