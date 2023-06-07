
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
    public float reloadTime;
    public bool isReloading = false;
    public GameObject impactEffect;
    public AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip reloadSound;
    public GameObject reloading;

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

            audioSource.PlayOneShot(reloadSound);
        reloading.SetActive(true);
            isReloading = true;
            yield return new WaitForSeconds(reloadTime);
            currentAmmo = maxAmmo;
            gameManager.ammo.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();
            isReloading =false;
        reloading.SetActive(false);
        
    }
    public Transform BulletSpawn;
    public GameObject BulletPrefab;
    public float BulletSpeed = 10f, DestroyBulletAfterSeconds = 2f;
    public void Shoot()
    {
        if (muzzleFlash != null) { muzzleFlash.Play(); }

        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
        currentAmmo--;
         gameManager.ammo.text = currentAmmo.ToString() + "/" + maxAmmo.ToString();
         weaponRecoil.RecoilFire();

         
        var bullet = (GameObject)Instantiate(BulletPrefab, BulletSpawn.position, BulletSpawn.rotation);

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * BulletSpeed;

        Destroy(bullet, DestroyBulletAfterSeconds);
    }

    internal void ModifyFireRate(float v)
    {
        fireRate += v;
    }

    internal void ModifyReloadSpeed(float v)
    {
        reloadTime -= v;
    }

    internal void ResetFireRate(float v)
    {
        fireRate -= v;
    }

    internal void ResetReloadSpeed(float v)
    {
        reloadTime += v;
    }
}
