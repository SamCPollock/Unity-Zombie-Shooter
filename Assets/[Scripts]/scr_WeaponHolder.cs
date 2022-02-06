using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_WeaponHolder : MonoBehaviour
{
    [Header("WeaponToSpawn"), SerializeField]

    GameObject weaponToSpawn;

    scr_PlayerController playerController;
    Sprite crosshairImage;

    [SerializeField]
    GameObject weaponSocket;

    void Start()
    {
        GameObject spawnedWeapon = Instantiate(weaponToSpawn, weaponSocket.transform.position, weaponSocket.transform.rotation, weaponSocket.transform);
    }

    void Update()
    {
        
    }
}
