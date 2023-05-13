
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;
    Transform gunTransform;
    float maxDistanceToTarget = 6f;
    float distanceToTarget;

    [SerializeField]
    GameObject Player;
    float rawDamage = 10f;

    [SerializeField]
    float delayTimerZombie = 2.5f;
    float delayTimerTurret = 2.5f;
    float tick;
    bool attackReady = true;

    [SerializeField]
    public float interval = 1.0f; // The duration in seconds between actions
    float ZombieSpeed = 3f;

    private float timer = 0.0f;
    bool IsCollidingPlayer = false;
    float delayTimer = 0f;
    //private HealthManager healthManager; 
    bool Attackready=false;
    void Start()
    {
        tick = delayTimer;
        gunTransform = gameObject.transform.Find("Gun");
        //healthManager = Player.GetComponent<HealthManager>();
    }

    void Update()
    {
        Attack();
        Attackready = AttackInterval();
    }

    bool IsReadyToAttack()
    {
      
        if (tick < delayTimer)
        {
            tick += Time.deltaTime;
            return false;
        }

        return true;
    }

    void LookAtTarget()
    {
        //this.transform.LookAt(playerTransform.position);
        Vector3 lookVector = playerTransform.position - transform.position;
        lookVector.y = transform.position.y;
        Quaternion rotation = Quaternion.LookRotation(lookVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.01f);
    }

    void Attack()
        
    {
        
        switch (gameObject.tag)
        {
            
            case "Turret"://stationary and shoots at player
                TurretAttack();
                break;
            case "Zombie"://Slow and walks to playe, damages on collide
                
                ZombieAttack();
                break;
                default:
                break;


        }
        
    }
    void TurretAttack()
    {
        distanceToTarget = Vector3.Distance(playerTransform.position, gunTransform.position);
        attackReady = IsReadyToAttack();

        if (distanceToTarget <= maxDistanceToTarget)
        {
            LookAtTarget();

            if (attackReady)
            {
                tick = 0f;
                Ray ray = new Ray(gunTransform.position, gunTransform.forward);
                RaycastHit raycastHit;

                if (Physics.Raycast(ray, out raycastHit, maxDistanceToTarget))
                {
                    Debug.Log("Enemy Shoots");
                    if (raycastHit.transform != null)
                    {
                        raycastHit.collider.SendMessageUpwards("Hit", rawDamage, SendMessageOptions.DontRequireReceiver);
                    }
                }
                else
                {
                    Debug.Log("ENEMY: FAILED RAYCAST");
                }
            }
        }
    }
    void ZombieAttack()
    {
       

        if (IsCollidingPlayer == false)
        {
            MoveTowardsPlayer();
        }
        else
        {
            
            if (Attackready == true)
            {
                timer = 0.0f;
                HealthManager healthManager = Player.GetComponent<HealthManager>();
                Debug.Log(healthManager.Gethitpoints());
                healthManager.Hit(rawDamage);
                Debug.Log("Zombie hit");
            }
        }
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("Collided");
            IsCollidingPlayer = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log(" stopped Collided");
            IsCollidingPlayer = false;
        }
    }
    //Checks if a player is colliding with an enemy and returns a boolean. used to prevent melee enemies from clipping
    void SetDelayTimer()
    {
        switch (gameObject.tag)
        {
            case "Turret"://stationary and shoots at player
                delayTimer = delayTimerTurret;
                break;
            case "Zombie"://Slow and walks to playe, damages on collide
                delayTimer = delayTimerZombie;
                break;
            default:
                break;


        }
    }
    bool AttackInterval()
    {
        timer += Time.deltaTime;

        // Check if the specified interval has elapsed
        if (timer >= interval)
        {
            
            //Returns true allowing an attack to happen
            return true;
            
            
            
        }
        else
        {
            return false;
        }
    }//creates an interval to attack 

    void MoveTowardsPlayer()
    {
        CharacterController controller = GetComponent<CharacterController>();
        Vector3 directionToPlayer = (playerTransform.transform.position - transform.position).normalized;
        controller.Move(directionToPlayer * ZombieSpeed * Time.deltaTime);

        // Apply gravity
        controller.Move(Vector3.down * ZombieSpeed * Time.deltaTime);
    }
    }
