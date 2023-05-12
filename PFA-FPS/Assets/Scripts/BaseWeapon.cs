
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    public GameManager gameManager;
    public WeaponData weaponData;
    float damage;
    float range ;
    float fireRate;
    float impactForce;
    public LayerMask layerToHit;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public int currentAmmo;
    public int maxAmmo;
    public int reloadTime;
    public bool isReloading = false;
    public GameObject impactEffect;
    

    public WeaponRecoil weaponRecoil;
    private float nextTimeToFire = 0f;

    public void Start()
    {
        
        weaponRecoil = GameObject.Find("CameraRot/CameraRecoil").GetComponent<WeaponRecoil>();

    }
    private void Awake()
    {
        gameManager = GameManager.Instance;
        damage = weaponData.damage;
        range = weaponData.range;   
        fireRate = weaponData.fireRate;
        impactForce = weaponData.impactForce;
        
        maxAmmo = weaponData.maxAmmo;
        reloadTime = weaponData.reloadTime;

        currentAmmo = maxAmmo;
        gameManager.ammo.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();
    }
   


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentAmmo>0 && isReloading==false)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        else if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentAmmo==0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            StartCoroutine(ReloadGun());
            StopCoroutine(ReloadGun()); 
        }
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            if (!isReloading) 
            {
                StartCoroutine(ReloadGun());
            }

        }
    }

    IEnumerator ReloadGun()
    {
        
   
            isReloading = true;
            yield return new WaitForSeconds(reloadTime);
            currentAmmo = maxAmmo;
            gameManager.ammo.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();
            isReloading =false;
        
    }

    private void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        currentAmmo--;
        gameManager.ammo.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();
        weaponRecoil.RecoilFire();
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, layerToHit))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            /*if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }*/

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
}
