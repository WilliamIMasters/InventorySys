using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Container : ScriptableObject
{
    public UnityEvent ContainerChanged;


    [SerializeField]
    private List<Item> items;
    public int maxCapacity;
    public string id { get; }

    public Container(int maxCapacity)
    {
        //generates unique id
        this.id = Guid.NewGuid().ToString();

        this.maxCapacity = maxCapacity;
        items = new List<Item>();
        ContainerChanged = new UnityEvent();
    }
    public Container(List<Item> items, int maxCapacity)
    {
        this.items = items;
        this.maxCapacity = maxCapacity;
        ContainerChanged = new UnityEvent();
    }

    public List<Item> GetItems()
    {
        return items;
    }

    private void Start()
    {
        items ??= new List<Item>();
        if (maxCapacity == 0) maxCapacity = 16;
    }

    public void AddItem(Item item)
    {
        if (items == null) items = new List<Item>();


        if(item is StackableItem) {

            var itemS = (StackableItem)item;

            var existingItemIndex = items.FindIndex(i => i.id == item.id && ((StackableItem)i).qty < ((StackableItem)i).maxStack);

            if(existingItemIndex != -1) { // if existing item is found 

                int newItemQty = itemS.qty;
                int existingItemQty = ((StackableItem)items[existingItemIndex]).qty;
                int maxStack = ((StackableItem)items[existingItemIndex]).maxStack;

                int overflow = (newItemQty + existingItemQty) - maxStack;

                if(overflow > 0) {
                    ((StackableItem)items[existingItemIndex]).qty = maxStack;

                    var newItem = itemS;
                    newItem.qty = overflow;
                    AddItem(newItem);

                } else {
                    ((StackableItem)items[existingItemIndex]).qty += itemS.qty;
                }

            } else {
                AddNewItem(item);
            }

            

        } else {
            AddNewItem(item);
        }

        ContainerChanged?.Invoke();
    }

    public void ClearItems()
    {
        items = new List<Item>();
        ContainerChanged.Invoke();
    }

    public Item GetItemAtIndex(int i)
    {
        if (i >= items.Count) return null;
        return items[i];
    }


    private void AddNewItem(Item item)
    {
        if(items.Count < maxCapacity) {
            items.Add(item);
        }
    }

    internal void RemoveItem(Item item)
    {

        items.Remove(item);
    }
}
