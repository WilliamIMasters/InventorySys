using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public abstract class Item : ScriptableObject{
    
    [SerializeField]
    public string name;
    public string description;
    public int id;
}
