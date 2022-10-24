using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerManager : MonoBehaviour
{
    public GameObject slotPrefab;

    public Container container;

    public List<InventorySlot> inventorySlots = new List<InventorySlot>();


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
        inventorySlots = new List<InventorySlot>();
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
            if (itemAtIndex != null)
                inventorySlots[i].DrawSlot(itemAtIndex);
        }
    }

    private void CreateEmptyInventorySlot()
    {
        GameObject newSlot = Instantiate(slotPrefab);
        newSlot.transform.SetParent(transform, false);

        InventorySlot newSlotComponent = newSlot.GetComponent<InventorySlot>();
        newSlotComponent.ClearSlot();

        inventorySlots.Add(newSlotComponent);

    }
}
