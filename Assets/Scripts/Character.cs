using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float health;
    
    public Container inventory;

    private int inventorySize = 25;

    private void Awake()
    {
        if(inventory == null) {
            inventory = new Container(inventorySize);
        }
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if(health < 0)
        {
            Die();
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
