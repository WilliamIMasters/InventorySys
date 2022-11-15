using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    // Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    // bools
    public bool shooting, readyToShoot, reloading;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit hit;
    public LayerMask shootable;

    // Camera Shake
    public GunShotRecoilController camShake;
    public float shakeMagnitude;
    public GameObject bulletHit;
    public MuzzleFlashController flashController;


    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    void Update()
    {
        MyInput();    
    }



    private void MyInput()
    {
        if (allowButtonHold) {
            shooting = Input.GetKey(KeyCode.Mouse0);
        } else {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) {
            Reload();
        }


        // shoot
        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }

    }

    private void Shoot()
    {
        Debug.Log("Shoot");
        readyToShoot = false;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 shootDirWithSpread = fpsCam.transform.forward + new Vector3(x, y, 0);

        //RayCast
        if (Physics.Raycast(fpsCam.transform.position, shootDirWithSpread, out hit, range, shootable))
        {
            Debug.Log(hit.collider.name);

            if(hit.collider.CompareTag("Character"))
            {
                hit.collider.GetComponent<Character>().TakeDamage(damage);
            }
        }

        // Sounds + camera shake + vfx
        ShotSideEffects();

        bulletsLeft--;
        bulletsShot--;
        Invoke("ResetShot", timeBetweenShooting);


        if(bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }

    private void ShotSideEffects()
    {
        // Camera shake
        camShake.Shot(shakeMagnitude, shakeMagnitude/2);
        
        // Instantiaets bulletHole
        Instantiate(bulletHit, hit.point + hit.normal * 0.001f, Quaternion.LookRotation(hit.normal, Vector3.up) * bulletHit.transform.rotation);

        flashController.Fire();
    }


    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }




}
