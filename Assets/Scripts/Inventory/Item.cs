using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public abstract class Item : ScriptableObject{
    
    [SerializeField]
    public string name;
    public string description;
    public int id;

    public Sprite icon;

    public virtual string GetDisplayMessage()
    {
        return name;
    }
}
