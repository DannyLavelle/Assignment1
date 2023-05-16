
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Transform cameraTransform;
    float range = 1000f;
    
    [SerializeField]
    float rawDamage = 10f;
    [SerializeField]
    
    public bool Weapon2Unlocked = false;
    int Ammo = 100;
    int Clip = 6;
    public int MaxClip = 6;

    [SerializeField]
    public GameObject EnergyProjectile;
    public GameObject HUD;
    public GameObject Menu;

    int CurrentWeapon = 1;

    bool ReadyToFire = true;
    void Update()
    {
        GetCurrentWeapon();

        InGameMenuController UI = Menu.GetComponent<InGameMenuController>();
        if (UI.GetIsGamePaused() == false)
        {
            FireWeapon();
            Reload();
        }

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
                        if (Clip > 0)
                        {
                            FirePistol();
                            Clip--;
                        }

                        break;
                    case 2:
                        if (Clip >= 5)
                    {
                        //FireEnergyWeapon();
                        Clip -= 5;
                    }
                    
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
        if (Input.GetKeyDown(KeyCode.Alpha1) )
        {
            CurrentWeapon = 1;
            UpdateClipsize();
            hud.ChangeWeapon(1);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && Weapon2Unlocked == true)
        {
          
                CurrentWeapon = 2;
            UpdateClipsize();
                hud.ChangeWeapon(2);
           

        }

    }
    void FirePistol()
    {


        cameraTransform = Camera.main.transform;
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit raycastHit;

        int layerMask = ~LayerMask.GetMask("Player");  // Exclude the "Player" layer

        if (Physics.Raycast(ray, out raycastHit, range, layerMask))
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
        //cameraTransform = Camera.main.transform;
        //Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        //RaycastHit raycastHit;

        //if (Physics.Raycast(ray, out raycastHit, range))
        //{
        //    if (raycastHit.transform != null)
        //    {
        //        raycastHit.collider.SendMessageUpwards("Hit", rawDamage, SendMessageOptions.DontRequireReceiver);

        //    }
        //}
        //else
        //{
        //    Debug.Log("NO RAYCAST FROM PLAYER ATTACK");
        //}
    }
    void Reload()
    {
      if(Input.GetKeyDown(KeyCode.R) || Input.GetMouseButtonDown(1))
        {
            switch (CurrentWeapon)
            {
                case 1:
                    if (Clip < MaxClip)
                    {
                        ReadyToFire = false;
                        Debug.Log("Reloading");
                        StartCoroutine(WaitAndReload(2));
                    }
                    break;
                case 2:
                if (Clip < MaxClip)
                {
                    ReadyToFire = false;
                    Debug.Log("Reloading");
                    StartCoroutine(WaitAndReload(5));
                }
                break;
                break;
                default:
                    break;
            }
        }
    }

    IEnumerator WaitAndReload(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        Ammo = Ammo - (MaxClip - Clip);
        Clip = MaxClip;
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
    public void UpdateClipsize()
    {
        switch (CurrentWeapon)
        {
            case 1:
            Ammo = Ammo + Clip;
            MaxClip = 6;
            Clip = 6;
                Ammo = Ammo - MaxClip;
            break;
            case 2:
            Ammo = Ammo + Clip;
            MaxClip = 5;
            Clip = 5;
            Ammo = Ammo - MaxClip;
           
            break;
            default:
        
            break;

        }

    }
    public int GetAmmoSize()
    {
        return Ammo;

    }
    public void AddDamage(float amount)
    {
        rawDamage += amount;
        Debug.Log("New Damage is " + rawDamage);
    }
    public void AddAmmo (int amount)
    {
        Ammo = Ammo + amount;
    }
    public int GetAmmo()
    {
        return Ammo;
    }
    //void FireEnergyWeapon()
    //{
       
    //Instantiate(EnergyProjectile,Camera.main.transform.position, Camera.main.transform.rotation);
    //}
    public int GetClipsize()
    {
        return Clip;
    }
        
}