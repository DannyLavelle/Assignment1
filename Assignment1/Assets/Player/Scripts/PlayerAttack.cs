
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Transform cameraTransform;
    float range = 100f;

    [SerializeField]
    float rawDamage = 10f;
    [SerializeField]
    bool Weapon1Unlocked = true;
    bool Weapon2Unlocked = false;
    int PistolAmmo = 100;
    int PistolClip = 6;
    int PistolMaxClip = 6;

    [SerializeField]
    public GameObject HUD;
    int CurrentWeapon = 1;

    bool ReadyToFire = true;
    void Update()
    {
        FireWeapon();
        Reload();
    }

    void FireWeapon()
    {
        if (ReadyToFire)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                switch (CurrentWeapon)
                {
                    case 1:
                        if (PistolClip > 0)
                        {
                            FirePistol();
                            PistolClip--;
                        }

                        break;
                    case 2:
                        //FireEnergyWeapon()
                        break;
                    default:
                        break;

                }
            }
        }
    }
    void GetCurrentWeapon()
    {
        HUDController hud = HUD.GetComponent<HUDController>();
        if (Input.GetKeyDown(KeyCode.Alpha1) && Weapon1Unlocked == true)
        {
            CurrentWeapon = 1;
            hud.ChangeWeapon(1);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && Weapon2Unlocked == true)
        {
            CurrentWeapon = 2;
            hud.ChangeWeapon(2);
        }

    }
    void FirePistol()
    {

        cameraTransform = Camera.main.transform;
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit, range))
        {
            if (raycastHit.transform != null)
            {
                raycastHit.collider.SendMessageUpwards("Hit", rawDamage, SendMessageOptions.DontRequireReceiver);

            }
        }
        else
        {
            Debug.Log("NO RAYCAST FROM PLAYER ATTACK");
        }
    }
    void Reload()
    {
      if(Input.GetKeyDown(KeyCode.R))
        {
            switch (CurrentWeapon)
            {
                case 1:
                    if (PistolClip < PistolMaxClip)
                    {
                        ReadyToFire = false;
                        Debug.Log("Reloading");
                        StartCoroutine(WaitAndReload(2));
                    }
                    break;
                case 2:
                    break;
                default:
                    break;
            }
        }
    }

    IEnumerator WaitAndReload(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        PistolAmmo = PistolAmmo - (PistolMaxClip - PistolClip);
        PistolClip = PistolMaxClip;
        ReadyToFire = true;
        Debug.Log("Finished Reloading");
    }

    void wait(float WaitTime)
    {
        float timer = WaitTime;
        while (timer > 0f)
        {
            timer = timer - Time.deltaTime;
        }
    }
    public int GetClipsize()
    {
        switch (CurrentWeapon)
        {
            case 1:
                return PistolClip;
                break;
            case 2:
                return 0;
                break;
            default:
                return 0;
                break;

        }

    }
    public int GetAmmoSize()
    {
        switch (CurrentWeapon)
        {
            case 1:
                return PistolAmmo;
                break;
            case 2:
                return 0;
                break;
            default:
                return 0;
                break;
        }

    }
}