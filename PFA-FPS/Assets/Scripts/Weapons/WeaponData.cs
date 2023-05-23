using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDatat", menuName = "Data/WeaponData")]
public class WeaponData : ScriptableObject
{
    #region Singleton

    private static WeaponData instance;

    public static WeaponData Instance
    {
        get
        {
            instance = instance ?? Resources.Load<WeaponData>("ManagerSingleto,/WeaponData");
            Debug.Log(instance);
            return instance;
        }
    }
    #endregion
    // Start is called before the first frame update


    

    public float damage;
    public float range ;
    public float fireRate ;
    public float impactForce ; 
    
    

    public float recoilX;
    public float recoilY;
    public float recoilZ;
    public float snappiness;
    public float returnSpeed;


   
    public int maxAmmo;
    public int reloadTime;


}
