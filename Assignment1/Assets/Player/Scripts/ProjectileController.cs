using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed = 10f; // Adjust the speed of the projectile
    public float rawdamage = 50f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed; // Set the initial velocity of the projectile
    }
    private void Update()
    {
        Destroy(gameObject, 5f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Zombie" || collision.gameObject.tag == "Turret")
        {
            HitManager hit = collision.transform.GetComponent<HitManager>();
            if (hit != null)
            {
                hit.Hit(rawdamage);
            }
            Destroy(gameObject);
        }
    }


   

}
