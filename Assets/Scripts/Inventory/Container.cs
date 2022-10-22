using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{

    [SerializeField]
    private List<Item> container;
    public int maxCapacity;

    public Container(int maxCapacity)
    {
        this.maxCapacity = maxCapacity;
    }
    public Container(List<Item> items, int maxCapacity)
    {
        this.container = items;
        this.maxCapacity = maxCapacity;
    }

    private void Start()
    {
        container = new List<Item>();
        container.Add(new Consumable("Apple", "An apple", 0, 15, 16, new ConsumableEffect[0]));

        if (maxCapacity == 0) maxCapacity = 16;
    }

    public void AddItem(Item item)
    {
        if (container == null) container = new List<Item>();


        if(item is StackableItem) {

            var itemS = (StackableItem)item;

            var existingItemIndex = container.FindIndex(i => i.id == item.id && ((StackableItem)i).qty < ((StackableItem)i).maxStack);

            if(existingItemIndex != -1) { // if existing item is found 

                int newItemQty = itemS.qty;
                int existingItemQty = ((StackableItem)container[existingItemIndex]).qty;
                int maxStack = ((StackableItem)container[existingItemIndex]).maxStack;

                int overflow = (newItemQty + existingItemQty) - maxStack;

                if(overflow > 0) {
                    ((StackableItem)container[existingItemIndex]).qty = maxStack;

                    var newItem = itemS;
                    newItem.qty = overflow;
                    AddItem(newItem);

                } else {
                    ((StackableItem)container[existingItemIndex]).qty += itemS.qty;
                }

            } else {
                AddNewItem(item);
            }

            

        } else {
            AddNewItem(item);
        }

        // Update UI;
    }

    private void AddNewItem(Item item)
    {
        if(container.Count < maxCapacity) {
            container.Add(item);
        }
    }



}
