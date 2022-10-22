using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerTest : MonoBehaviour
{
    private Container container;

    private Item testItem;

    private void Start()
    {
        this.container = GetComponent<Container>();

        testItem = new Consumable("Apple","An apple", 0, 3, 16, new ConsumableEffect[0]);

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse2)) {
            container.AddItem(testItem);
        }
    }
}
