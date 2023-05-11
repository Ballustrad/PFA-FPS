
using System;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    public WeaponData weaponData;
    float damage;
    float range ;
    float fireRate;
    float impactForce;
    public LayerMask layerToHit;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
   
    public GameObject impactEffect;
    

    public WeaponRecoil weaponRecoil;
    private float nextTimeToFire = 0f;

    private void Awake()
    {
        damage = weaponData.damage;
        range = weaponData.range;   
        fireRate = weaponData.fireRate;
        impactForce = weaponData.impactForce;
        
        
       
    }
    public void Start()
    {
        weaponRecoil = GameObject.Find("CameraRot/CameraRecoil").GetComponent<WeaponRecoil>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        weaponRecoil.RecoilFire();
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, layerToHit))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
}
