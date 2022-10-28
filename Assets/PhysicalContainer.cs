using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalContainer : MonoBehaviour, IIntractable
{
    public int containerSize;

    private Container container;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetContainer(Container container)
    {
        this.container = container;
        this.containerSize = container.maxCapacity;
    }

    public string GetDisplayMessage()
    {
        return "Open";
    }
}
