using System.Collections;
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
            CreateEmptyInventorySlot();
            Item itemAtIndex = container.GetItemAtIndex(i);
            if (itemAtIndex != null) {
                inventorySlots[i].AddItemSlot(itemAtIndex);
                //inventorySlots[i].slot?.DrawSlot(itemAtIndex);

            }
        }
    }

    private void CreateEmptyInventorySlot()
    {
        GameObject newSlot = Instantiate(emptySlotPrefab);
        newSlot.transform.SetParent(transform, false);

        ItemSlotManager newSlotComponent = newSlot.GetComponent<ItemSlotManager>();
        newSlotComponent.ClearSlot();

        inventorySlots.Add(newSlotComponent);

    }
}
