using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ContainerManager : MonoBehaviour
{
    public GameObject slotPrefab;
    public GameObject emptySlotPrefab;

    public Container container;

    public List<ItemSlotManager> inventorySlots = new List<ItemSlotManager>();


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O)) {
            DrawContainer();

        }
    }

    private void Start()
    {
        container.ContainerChanged.AddListener(DrawContainer);


        DrawContainer();
    }

    public void ClearContainerUI()
    {
        inventorySlots = new List<ItemSlotManager>();
        foreach (Transform  child in transform) {
            Destroy(child.gameObject);
        }
    }

    public void DrawContainer()
    {
        ClearContainerUI();

        for(int i=0; i < container.maxCapacity; i++) {
            CreateEmptyInventorySlot(i);
            /*Item itemAtIndex = container.GetItemAtIndex(i);
            if (itemAtIndex != null) {
                inventorySlots[i].AddItemSlot(itemAtIndex);
                //inventorySlots[i].slot?.DrawSlot(itemAtIndex);

            }*/
        }
        var itemsToAddAfter = new List<Item>();
        foreach (Item item in container.GetItems()) {
            if (item.prefUISlot != null && inventorySlots[(int)item.prefUISlot].IsEmpty()) {

                inventorySlots[(int)item.prefUISlot].AddItemSlot(item);

            } else {
                itemsToAddAfter.Add(item);
            }
        }
        foreach(Item item in itemsToAddAfter) {
            inventorySlots[GetFirstEmptyIndex()].AddItemSlot(item);
        }
    }

    private int GetFirstEmptyIndex()
    {
        return inventorySlots.FirstOrDefault(i => i.IsEmpty()).index;
    }

    private void CreateEmptyInventorySlot(int i)
    {
        GameObject newSlot = Instantiate(emptySlotPrefab);
        newSlot.transform.SetParent(transform, false);

        ItemSlotManager newSlotComponent = newSlot.GetComponent<ItemSlotManager>();
        newSlotComponent.ClearSlot();
        newSlotComponent.index = i;

        inventorySlots.Add(newSlotComponent);

    }
}
