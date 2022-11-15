using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FPSController))]
public class GunShotRecoilController : MonoBehaviour
{

    public GameObject player;
    public GameObject playerCamera;

    private FPSController fps;

    // Start is called before the first frame update
    public void Shot(float magX, float magY)
    {
        float x = -magX * Random.Range(0.75f, 1f);
        float y = -magY * Random.Range(-1f, 1f);

        fps.AddRecoil(x);//playerCamera.transform.localEulerAngles = playerCamera.transform.localEulerAngles + new Vector3(x, 0, 0);
        player.transform.eulerAngles = player.transform.eulerAngles - new Vector3(0, y, 0);
    }

    private void Start()
    {
        fps = player.GetComponent<FPSController>();
    }

    public void Update()
    {
        
    }
}
