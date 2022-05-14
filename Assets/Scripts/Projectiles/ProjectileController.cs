using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileController : MonoBehaviour
{
    protected Rigidbody projectileRb;
    protected Transform player;
    protected GameObject playerObject; 
    [SerializeField] protected float speed;
    public GameObject explosion; 

    protected virtual void Start()
    {
        Initialize(); 
    }

    protected virtual void Update()
    {
        DestroyIfPlayerInactive(); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Player" && !collision.gameObject.CompareTag("Projectile") && !collision.gameObject.CompareTag("Turret"))
        {
            DestroyProjectile(); 
        }
    }

    public void DestroyIfPlayerInactive()
    {
        if (playerObject.activeInHierarchy == false)
        {
            DestroyProjectile(); 
        }

        if (!playerObject.GetComponent<PlayerController>().canMove)
        {
            DestroyProjectile(); 
        }
    }

    protected void DestroyProjectile()
    {
        Instantiate(explosion, transform.position, explosion.transform.rotation); 
        Destroy(gameObject);
    }

    protected virtual void Initialize()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        playerObject = GameObject.Find("Player"); 
        projectileRb = GetComponent<Rigidbody>();
        transform.LookAt(playerObject.transform);
        projectileRb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }
}
