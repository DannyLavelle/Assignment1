using System.Collections;
using System.Collections.Generic;
//using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using TMPro;
using System.Diagnostics;
using UnityEngine.UI;
using System;

public class HUDController : MonoBehaviour
{
    [SerializeField]
    public TMP_Text CurrentWeaponText;
    public TMP_Text AmmoText;
    public Image CurrentWeaponImage;
    public Sprite PistolSprite;
    public Sprite EnergySprite;
    // Start is called before the first frame update
    [SerializeField]
    GameObject Player;

    // Update is called once per frame
    void Update()
    {
        UpdateAmmo();
    }
    public void ChangeWeapon(int WeaponNumber)
    {
       switch(WeaponNumber)
        {
            case 1:
                CurrentWeaponText.text = "Pistol";
               
                    
             break;
            case 2:
                CurrentWeaponText.text = "Energy Weapon";
                

                break;

        }
    }

    void UpdateAmmo()
    {

        PlayerAttack Ammo = Player.GetComponent<PlayerAttack>();
        int Clipsize = Ammo.GetClipsize();
        int AmmoSize = Ammo.GetAmmoSize();
        AmmoText.text = Clipsize.ToString() + "/" + AmmoSize.ToString();
    }
}
