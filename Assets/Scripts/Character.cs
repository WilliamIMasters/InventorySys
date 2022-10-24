using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    float health;
    Container inventory;

    private void Start()
    {
        inventory = GetComponent<Container>();
        if(inventory == null) {
            Debug.LogError("No container component on " + gameObject.name);
        }
    }
}
