using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{

    public float timeUntilDestroy = 30;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destory", timeUntilDestroy);
    }

    
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
