using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableComponent : MonoBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public event Action<PointerEventData> OnBeginDragHandler;
    public event Action<PointerEventData> OnDragHandler;
    public event Action<PointerEventData, bool> OnEndDragHandler;

    public bool FollowCursor { get; set; } = true;
    public Vector3 startPosition;
    public Container initalContainer;
    public ItemSlotManager initalItemSlotManager;
    public bool CanDrag { get; set; } = true;

    private RectTransform rectTransform;
    [SerializeField]
    private Canvas canvas;
    private ItemSlot itemSlot;


    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.Find("PlayerUI").GetComponent<Canvas>();
        itemSlot = GetComponent<ItemSlot>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!CanDrag) {
            return;
        }
        
        OnBeginDragHandler?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!CanDrag) {
            return;
        }

        OnDragHandler?.Invoke(eventData);

        if (FollowCursor) {
            GetComponent<RectTransform>().anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(!CanDrag) {
            return;
        }

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        DropArea dropArea = null;

        foreach(var result in results) {
            dropArea = result.gameObject.GetComponent<DropArea>();

            if(dropArea !=null) {
                break;
            }
        }

        if(dropArea != null) {
            if(dropArea.Accepts(this)) {
                

                Container droppedContainer = transform.parent.parent.GetComponent<ContainerManager>().container;
                var newItemSlotManager = dropArea.GetComponent<ItemSlotManager>();
                
                
                
                // if spot empty add item
                if (newItemSlotManager.IsEmpty()) {
                    // drops item
                    dropArea.Drop(this);
                    OnEndDragHandler?.Invoke(eventData, true);

                    // adds slot to new slot manager
                    newItemSlotManager.slot = initalItemSlotManager.slot;
                    initalItemSlotManager.slot = null;

                    // switches container if different
                    if (initalContainer == null) {
                        throw new Exception();
                    }
                    if (!(initalContainer.id == droppedContainer.id)) {
                        droppedContainer.AddItem(itemSlot.item);
                        initalContainer.RemoveItem(itemSlot.item);
                    }

                    return;
                } else { // else swap items


                    // moves other slot to slot manager
                    var oldItem = newItemSlotManager.transform.GetChild(0);//.SetParent(initalItemSlotManager.transform);
                    oldItem.SetParent(initalItemSlotManager.transform, false);


                    // drops item
                    dropArea.Drop(this);
                    OnEndDragHandler?.Invoke(eventData, true);

                    // swaps slot managers
                    newItemSlotManager.slot = initalItemSlotManager.slot;
                    var tmp = newItemSlotManager.slot;
                    initalItemSlotManager.slot = tmp;

                    // swaps containers if different
                    if (initalContainer == null) {
                        throw new Exception();
                    }
                    if (!(initalContainer.id == droppedContainer.id)) {
                        droppedContainer.AddItem(itemSlot.item);
                        initalContainer.RemoveItem(itemSlot.item);

                        var otherItem = initalItemSlotManager.slot.item;

                        droppedContainer.RemoveItem(otherItem);
                        initalContainer.AddItem(otherItem);
                    }

                    return;

                }
                

            }
        }

        rectTransform.anchoredPosition = startPosition;
        OnEndDragHandler?.Invoke(eventData, false);
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        startPosition = rectTransform.anchoredPosition;

        initalContainer = transform.parent.parent.GetComponent<ContainerManager>().container;

        initalItemSlotManager = transform.parent.GetComponent<ItemSlotManager>();
    }

}
