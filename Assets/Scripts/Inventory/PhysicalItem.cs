using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalItem : MonoBehaviour, IIntractable
{

    [SerializeField]
    private int itemId =-1;
    [SerializeField]
    private int qty;

    private Item item;

    private void Awake()
    {

        GetItemFromDb();

        
    }

    public Item GetItem()
    {

        if(item == null) {
            GetItemFromDb();
        }

        return item;
    }

    public void RemoveItem()
    {
        gameObject.active = false;
    }

    public string GetDisplayMessage()
    {
        return item.GetDisplayMessage();
    }

    private void GetItemFromDb()
    {
        item = ItemDataBase.GetItemById(itemId);
        if (item is StackableItem) {
            ((StackableItem)item).qty = this.qty;
        }
    }
}
