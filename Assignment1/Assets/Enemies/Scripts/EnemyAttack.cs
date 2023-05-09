
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;
    Transform gunTransform;
    float maxDistanceToTarget = 6f;
    float distanceToTarget;

    [SerializeField]
    float rawDamage = 10f;

    [SerializeField]
    float delayTimerZombie = 2.5f;
    float delayTimerTurret = 2.5f;
    float tick;
    bool attackReady = true;

    bool IsCollidingPlayer = false;
    float delayTimer = 0f;
    void Start()
    {
        tick = delayTimer;
        gunTransform = gameObject.transform.Find("Gun");
    }

    void Update()
    {
        Attack();
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
        if (IsCollidingPlayer = false)
        { 
            float speed = 3f;
        Vector3 directionToPlayer = (playerTransform.transform.position - transform.position).normalized;
        transform.position += directionToPlayer * speed * Time.deltaTime;
        }
        else
        {

        }
        
    }

    private void OnTriggerStay(Collider collider)
    {
       if (collider.tag == "player")
        {
            IsCollidingPlayer = true;
        }
       else
        {
            IsCollidingPlayer = false;
        }
    }
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
}
