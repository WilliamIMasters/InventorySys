using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlashController : MonoBehaviour
{

    private void Start()
    {
        HideFlash();
    }

    public void Fire()
    {
        if(!transform.GetChild(0).gameObject.activeSelf)
        {
            ShowFlash();
            Invoke("HideFlash", 0.1f);
        }
    }

    private void ShowFlash()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void HideFlash()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
