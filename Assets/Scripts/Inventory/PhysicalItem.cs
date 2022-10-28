using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalItem : MonoBehaviour, IIntractable
{

    [SerializeField]
    private Item itemScriptable;
    [SerializeField]
    private int qty;

    private Item item;

    private void Awake()
    {
        item = Instantiate(itemScriptable);
        if(item is StackableItem) {
            ((StackableItem)item).qty = this.qty;
        }
    }

    public Item GetItem()
    {
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
}
