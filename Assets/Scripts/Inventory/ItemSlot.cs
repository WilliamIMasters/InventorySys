using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ItemSlot : MonoBehaviour
{

    public Item item;

    //public Image icon;
    public Image icon;
    public TMP_Text qty;
    public Image qtyBG;



    public void ClearSlot()
    {
        icon.enabled = false;
        qty.enabled = false;
        qtyBG.enabled = false;
        item = null;
    }

    public void  DrawSlot(Item item)
    {
        if(item == null) {
            ClearSlot();
            return;
        }


        this.item = item;

        EnableSlot();
        icon.sprite = item.icon;
        qty.text = item is StackableItem ? ((StackableItem)item).qty.ToString() : "";
        qtyBG.enabled = qty.text == "" ? false : true;
    }

    
    private void EnableSlot()
    {
        icon.enabled = true;
        qty.enabled = true;
    }

}
