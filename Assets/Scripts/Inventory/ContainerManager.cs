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



    private void Start()
    {
        if (container != null) {
            container?.ContainerChanged.AddListener(DrawContainer);
            DrawContainer();
        }
    }

    public void SetContainer(Container container)
    {
        if(container == null) {
            Debug.LogError("ContainerManager.cs - SetContainer(container) - container was null");
            return;
        }

        this.container = container;
        this.container.ContainerChanged.AddListener(DrawContainer);
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
        if(container == null) {
            return;
        }

        for(int i=0; i < container.maxCapacity; i++) {
            CreateEmptyInventorySlot(i);
        }
        
        // Adds items to ui
        var itemsToAddAfter = new List<Item>();
        foreach (Item item in container.GetItems()) {
            if (item.prefUISlot != -1 && inventorySlots[item.prefUISlot].IsEmpty()) {

                inventorySlots[(int)item.prefUISlot].AddItemSlot(item);

            } else {
                itemsToAddAfter.Add(item);
            }
        }  
        
        // any items that didnt have a pref position on  ui added afterwards
        foreach(Item item in itemsToAddAfter) {
            var emptyIndex = GetFirstEmptyIndex();
            inventorySlots[emptyIndex].AddItemSlot(item);
            item.prefUISlot = emptyIndex;
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
