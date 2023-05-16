
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    [SerializeField]
    GameObject itemPrefab;
    public GameObject Menu;
    [SerializeField]
    Sprite icon;

    [SerializeField]
    string itemName;
    [SerializeField]
    [TextArea(4, 16)]
    string description;

    [SerializeField]
    float weight = 0;
    [SerializeField]
    int quantity = 1;
   

    [SerializeField]
    bool isStorable = false; // if false, item will be used on pickup
    [SerializeField]
    bool isConsumable = true; // if true, item will be destroyed (or quantity reduced) when used

    [SerializeField]
    bool isPickupOnCollision = false;

    [SerializeField]
    GameObject EnemySpawn;
    
    private void Start()
    {
        if (isPickupOnCollision)
        {
            gameObject.GetComponent<Collider>().isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        
                    InGameMenuController UI ;


        if (isPickupOnCollision)
        {
            if (collider.tag == "Player")
            {
                PlayerAttack Attack = collider.GetComponent<PlayerAttack>();
                HealthManager Health = collider.GetComponent<HealthManager>();
                switch (gameObject.tag)
                {
                    case "Trap":
                        Debug.Log("pain");
                        break;
                    case "Unlocked Door":
                        gameObject.SetActive(false);
                        break;
                    case "Enemy Door":
                    UI = Menu.GetComponent<InGameMenuController>();
                    UI.EnemyInfoShow();
                        EnemySpawn.SetActive(true);
                        gameObject.SetActive(false);
                        
                        break;
                    case "Goal":
                    UI = Menu.GetComponent<InGameMenuController>();
                    UI.WinSequence();
                        break;
                    case "Health":
                    Health.Heal(15f);
                        break;
                    case "Damage":
                    Attack.AddDamage(2.5f);
                    break;
                    case "Ammo":
                    Attack.AddAmmo(6);
                        break;

                    default:
                        Interact();
                        break;


                }
               GameObject.Destroy(gameObject);
                
            }
        }
    }

    public void Interact()
    {
        Debug.Log("Picked up " + transform.name);

        if (isStorable)
        {
            Store();
        }
        else
        {
            Use();
        }
    }

    void Store()
    {
        Debug.Log("Storing " + transform.name);
        InGameMenuController EnemyInfo = Menu.GetComponent<InGameMenuController>();
        // TODO Inventory system
        switch (gameObject.tag)
        {
            case "Trap":
                Debug.Log("pain");
                break;
            case "Unlocked Door":
                gameObject.SetActive(false);
                break;
            case "Enemy Door":
                EnemyInfo.EnemyInfoShow();
                EnemySpawn.SetActive(true);
                gameObject.SetActive(false);
                
                break;
            default:
                Destroy(gameObject);
                break;


        }
       
       
    }

    void Use()
    {
        Debug.Log("Using " + transform.name);
        if (isConsumable)
        {
            quantity--;
            if (quantity <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
