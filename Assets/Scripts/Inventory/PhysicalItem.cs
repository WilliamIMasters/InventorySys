using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalItem : MonoBehaviour
{

    [SerializeField]
    private Item item;
    [SerializeField]
    public int? id = null;
    [SerializeField]
    public int? qty = null;

    public Item GetItem()
    {
        return item;
    }

    public void RemoveItem()
    {
        gameObject.active = false;
    }
}
