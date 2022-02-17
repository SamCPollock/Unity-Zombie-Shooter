using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47Component : WeaponComponent
{

    protected override void FireWeapon()
    {
        Vector3 hitLocation;

        if (weaponStats.bulletsInClip > 0 && !isReloading && !weaponHolder.playerController.isRunning)
        {
            base.FireWeapon();
            if (firingEffect)
            {
                firingEffect.Play();
            }

            Ray screenRay = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
            if (Physics.Raycast(screenRay, out RaycastHit hit, weaponStats.fireDistance, weaponStats.weaponHitLayers))
            {
                hitLocation = hit.point;

                Vector3 hiteDirection = hit.point - mainCamera.transform.position;

                Debug.DrawRay(mainCamera.transform.position, hiteDirection.normalized * weaponStats.fireDistance, Color.red, 1.0f);
            }

            print("BulletInClip Count: " + weaponStats.bulletsInClip);

        }

        else if (weaponStats.bulletsInClip <= 0)
        {
            weaponHolder.StartReloading();
            Debug.Log("AUTORELOADING");
        }

    }
}
