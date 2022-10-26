using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotManager : MonoBehaviour
{
    public GameObject itemSlotPrefab;


    public ItemSlot slot;

    public int index;

    protected DropArea dropArea;

    protected virtual void Awake()
    {
        dropArea = GetComponent<DropArea>() ?? gameObject.AddComponent<DropArea>();
        dropArea.onDropHandler += OnItemDropped;
    }


    public void ClearSlot()
    {
        slot = null;
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
    }

    public void AddItemSlot(Item item)
    {
        var itemSlot = Instantiate(itemSlotPrefab);
        itemSlot.transform.SetParent(transform, false);
        slot = itemSlot.GetComponent<ItemSlot>();
        slot.DrawSlot(item);
    }

    public bool IsEmpty()
    {
        return slot == null;
    }

    private void OnItemDropped(DraggableComponent draggable)
    {
        draggable.transform.position = transform.position;
        draggable.transform.parent = transform;

        draggable.transform.gameObject.GetComponent<ItemSlot>().item.prefUISlot = index;
    }

    

}
