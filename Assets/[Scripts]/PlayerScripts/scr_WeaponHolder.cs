using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class scr_WeaponHolder : MonoBehaviour
{
    [Header("WeaponToSpawn"), SerializeField]

    GameObject weaponToSpawn;

    public scr_PlayerController playerController;
    Animator animator; 
    
    Sprite crosshairImage;

    WeaponComponent equippedWeapon;

    [SerializeField]
    GameObject weaponSocket;

    [SerializeField]
    Transform gripIKSocketLocation;


    // animator hashes

    public readonly int isFiringHash = Animator.StringToHash("IsFiring");
    public readonly int isReloadingHash = Animator.StringToHash("IsReloading");

    bool wasFiring = false;
    bool firingPressed = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<scr_PlayerController>();

        GameObject spawnedWeapon = Instantiate(weaponToSpawn, weaponSocket.transform.position, weaponSocket.transform.rotation, weaponSocket.transform);

        equippedWeapon = spawnedWeapon.GetComponent<WeaponComponent>();
        equippedWeapon.Initialize(this);
        gripIKSocketLocation= equippedWeapon.gripLocation; 
    }

    void Update()
    {
        
    }

    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, gripIKSocketLocation.transform.position);
    }


    public void OnFire(InputValue value)
    {
        firingPressed = value.isPressed;

        if (firingPressed)
        {
            StartFiring();
        }
        else
        {
            StopFiring();
        }


    }

    public void StartFiring()
    {
        if (equippedWeapon.weaponStats.bulletsInClip <= 0) return;

        animator.SetBool(isFiringHash, true);
        playerController.isFiring = true;
        equippedWeapon.StartFiringWeapon();
        

    }

    public void StopFiring()
    {
        animator.SetBool(isFiringHash, false);

        playerController.isFiring = false;
        equippedWeapon.StopFiringWeapon();


    }

    public void OnReload(InputValue value)
    {
        playerController.isReloading = value.isPressed;
        animator.SetBool(isReloadingHash, playerController.isReloading);
        equippedWeapon.weaponStats.bulletsInClip = equippedWeapon.weaponStats.clipSize;

    }

    public void StartReloading()
    {

    }    
}
