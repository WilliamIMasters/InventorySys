using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InteractionIndicator : MonoBehaviour
{
    public Image indicator;
    public TMP_Text tmp;


    public  void Display()
    {
        indicator.enabled = true;
    }

    public void Display(string message)
    {
        Display();
        tmp.text = message;
        
        tmp.enabled = true;
    }

    public void Hide()
    {
        indicator.enabled = false;
        tmp.enabled = false;
    }
}
