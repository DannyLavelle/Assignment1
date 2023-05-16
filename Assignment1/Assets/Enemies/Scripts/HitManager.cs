
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class HitManager : MonoBehaviour
{
    [SerializeField]
    float hitPoints = 25;
    public GameObject Ammo;
    public GameObject Health;
    public GameObject Damage;
    private EnemyAttack enemy ;
    public GameObject Player;
    public float HealthChance;
    public float DamageChance;
    public float AmmoChance;
    private void Start()
    {
        enemy = GetComponent<EnemyAttack>();
        Player = enemy.Player;
    }
    public void Hit(float rawDamage)
    {
        hitPoints -= rawDamage;
        if (hitPoints <= 0)
        {
            switch(DetermineDrop())
            {
                case 0:
                    break;
                case 1:
                Debug.Log("Dropping Ammo");
                Instantiate(Ammo, transform.position, transform.rotation);
                break;
                case 2:
                Debug.Log("Dropping Damage");
                Instantiate(Damage, transform.position, transform.rotation);
                break;
                case 3:
                Debug.Log("Dropping Health");
                Instantiate(Health, transform.position, transform.rotation);
                break;
                
            }
            SelfTerminate();

        }
    }

    void SelfTerminate()
    {
        Destroy(gameObject);
    }
    //int DetermineDrop()
    //{
    //    Debug.Log("Deciding Drop");
    //    float randomFloat = Random.Range(0f, 1f);
    //    Debug.Log("Rng is " + randomFloat);
    //    DetermineHealthChance();
    //    if (randomFloat > HealthChance + DamageChance + AmmoChance)
    //    {
    //        Debug.Log("Return 0");
    //        return 0;
    //    }
    //    else if (randomFloat > HealthChance + DamageChance) 
    //    {
    //        Debug.Log("Return 1");
    //        return 1;
    //    }
    //    else if(randomFloat > HealthChance)
    //    {
    //        Debug.Log("Return 2");
    //        return 2;
    //    }
    //    else 
    //    {
    //        Debug.Log("Return 3");
    //        return 3; 
    //    }






    //}
    int DetermineDrop()
    {
        Debug.Log("Deciding Drop");
        float randomFloat = Random.Range(0f, 1f);
        Debug.Log("Rng is " + randomFloat);
        DetermineHealthChance();

        float dropChance = randomFloat;
        if (dropChance < 0.1f)
        {
            Debug.Log("Return 0 (No drop)");
            return 0;
        }
        else if (dropChance < .1f + HealthChance)
        {
            Debug.Log("Return 3 (Health)");
            return 3;
        }
        else if (dropChance < .1f +HealthChance + AmmoChance)
        {
            Debug.Log("Return 1 (Ammo)");
            return 1;
        }
        else if (dropChance < .1f + HealthChance + AmmoChance + DamageChance)
        {
            Debug.Log("Return 2 (Damage)");
            return 2;
        }
      
        else { return 0; }
    }


    void DetermineHealthChance()
    {
        HealthManager PlayerHealth = Player.GetComponent<HealthManager>();
        float playerHealthPercent = PlayerHealth.Percentagehitpoints();

        HealthChance = .9f - (playerHealthPercent * 0.9f);
        DetermineAmmoChance();
    }
    //void DetermineHealthChance()
    //{
    //    HealthManager PlayerHealth = Player.GetComponent<HealthManager>();
    //    float playerHealthpercent = PlayerHealth.Percentagehitpoints();

    //    HealthChance = (.9f - playerHealthpercent);
    //    if (HealthChance < 0)
    //    {
    //        HealthChance = 0;
    //    }
    //    DetermineAmmoChance();
    //}
    //void DetermineAmmoChance()
    //{

    //    PlayerAttack playerAttack = Player.GetComponent<PlayerAttack>();
    //    int PlayerAmmo;
    //    if (playerAttack.GetAmmo()> 100)
    //    {
    //        PlayerAmmo = 100;
    //    }
    //    else
    //    {
    //        PlayerAmmo = playerAttack.GetAmmo();
    //    }

    //    AmmoChance = (.9f -((.9f - HealthChance) * (PlayerAmmo / 100)));
    //    DetermineDamageChance();
    //}
    void DetermineAmmoChance()
    {
        PlayerAttack playerAttack = Player.GetComponent<PlayerAttack>();
        int playerAmmo = Mathf.Min(playerAttack.GetAmmo(), 100);
        AmmoChance = ((.9f - HealthChance) * ( 1- (playerAmmo/100)));

        //AmmoChance = 0.2f + ((100f /playerAmmo ) * 0.3f);
        DetermineDamageChance();
    }
    void DetermineDamageChance()
    {
        DamageChance = .9f - HealthChance - AmmoChance;
    }
}
