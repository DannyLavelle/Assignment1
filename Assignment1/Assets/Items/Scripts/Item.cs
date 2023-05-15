
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
    int maxStackableQuantity = 1; // for bundles of items, such as arrows or coins

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
        InGameMenuController UI = Menu.GetComponent<InGameMenuController>();
        if (isPickupOnCollision)
        {
            if (collider.tag == "Player")
            { 
                switch(gameObject.tag)
                {
                    case "Trap":
                        Debug.Log("pain");
                        break;
                    case "Unlocked Door":
                        gameObject.SetActive(false);
                        break;
                    case "Enemy Door":
                        UI.EnemyInfoShow();
                        EnemySpawn.SetActive(true);
                        gameObject.SetActive(false);
                        
                        break;
                    case "Goal":
                        UI.WinSequence();
                        break;
                    default:
                        Interact();
                        break;


                }
               
                
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
