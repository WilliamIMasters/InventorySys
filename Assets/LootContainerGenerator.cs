using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PhysicalContainer))]
public class LootContainerGenerator : MonoBehaviour
{



    void Awake()
    {
        Container container = new Container(25);

        container.AddItem(ItemDataBase.GetItemById(0, 16));

        GetComponent<PhysicalContainer>().SetContainer(container);
    }
}
