using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShotRecoilOffSetter : MonoBehaviour
{
    // Start is called before the first frame update
    public void Shot(float magnitude)
    {
        float x = Random.Range(-1, 1) * magnitude;
        float y = Random.Range(-1, 1) * magnitude;

        //transform.localPosition = new Vector3(x, y, t.z);
        var rotateValue = new Vector3(x, y * -1, 0);

        transform.eulerAngles = transform.eulerAngles - rotateValue;
    }
}
