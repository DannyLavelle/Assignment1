
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    float Maxhitpoints = 100f;
    public Slider healthSlider;
    public GameObject Menu;
    [HideInInspector] 
    float hitPoints;
    
    private void Start()
    {
        hitPoints = Maxhitpoints;
    }
    public float Gethitpoints()
    {
        return hitPoints;   
    }
    public void Hit(float rawDamage)
    {
        InGameMenuController UI = Menu.GetComponent<InGameMenuController>();
        hitPoints -= rawDamage;
        SetHealthSlider();
        ;

        if (hitPoints <= 0)
        {
            UI.DeathSequence();
        }
    }
    void SetHealthSlider()
    {
        if (healthSlider != null)
        {
            healthSlider.value = Percentagehitpoints();
        }
    }

    public float Percentagehitpoints()
    {
        return hitPoints / Maxhitpoints;
    }
    public void Heal(float Ammount)
    {
        if (hitPoints + Ammount  <= Maxhitpoints)
        {
            hitPoints = hitPoints + Ammount;
        }
        else
        {
            hitPoints = Maxhitpoints;
        }
        SetHealthSlider();
    }
  
}
