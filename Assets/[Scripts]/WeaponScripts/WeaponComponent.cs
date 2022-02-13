using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    None, 
    Pistol, 
    MachineGun
}

public enum WeaponFiringPattern
{
    SemiAuto, FullAuto, ThreeShotBurst, FiveShotBurst
}

[System.Serializable]
public struct WeaponStats
{
    public WeaponType weaponType;
    public float damage;
    public int bulletsInClip;
    public int clipSize;

    public float fireStartDelay;
    public float fireRate;
    public float fireDistance;
    public bool repeating;
    public LayerMask weaponHitLayers;

    public WeaponFiringPattern weaponFiringPattern;

}

public class WeaponComponent : MonoBehaviour
{
    public Transform gripLocation;
    protected scr_WeaponHolder weaponHolder;

    [SerializeField]
    public WeaponStats weaponStats;

    public bool isFiring = false;
    public bool isReloading = false;

    public Camera mainCamera; 


    void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        
    }

    public void Initialize(scr_WeaponHolder _weaponHolder)
    {
        weaponHolder = _weaponHolder;

    }

    // decide weather rapid fire or not. 
    public virtual void StartFiringWeapon()
    {
        isFiring = true;
        if(weaponStats.repeating)
        {
            InvokeRepeating(nameof(FireWeapon), weaponStats.fireStartDelay, weaponStats.fireRate);
        }
        else
        {
            FireWeapon();
        }
    }

    public virtual void StopFiringWeapon()
    {
        isFiring = false;
        CancelInvoke(nameof(FireWeapon));

    }

    protected virtual void FireWeapon()
    {
        print("Firing Weapon!");
        weaponStats.bulletsInClip--;
    }


}
