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
    public bool CanDrag { get; set; } = true;

    private RectTransform rectTransform;
    [SerializeField]
    private Canvas canvas;


    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.Find("PlayerUI").GetComponent<Canvas>();
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
                dropArea.Drop(this);
                OnEndDragHandler?.Invoke(eventData, true);
                return;
            }
        }

        rectTransform.anchoredPosition = startPosition;
        OnEndDragHandler?.Invoke(eventData, false);
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        startPosition = rectTransform.anchoredPosition;
    }

}
