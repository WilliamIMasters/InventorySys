using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public abstract class Item : ScriptableObject, ICloneable
{
    
    [SerializeField]
    public string name;
    public string description;
    public int id;


    public Sprite icon;
    public int prefUISlot = -1;

    protected Item(string name, string description, int id, Sprite icon)
    {
        this.name = name;
        this.description = description;
        this.id = id;
        this.icon = icon;
    }

    public object Clone()
    {
        return this.MemberwiseClone();
    }

    public virtual string GetDisplayMessage()
    {
        return name;
    }

}
