using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileController : MonoBehaviour
{
    protected Rigidbody projectileRb;
    protected Transform player;
    [SerializeField] protected float speed;
    public GameObject explosion; 

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        projectileRb = GetComponent<Rigidbody>();
        transform.LookAt(player);
        projectileRb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    public virtual void Update()
    {
        DestroyIfPlayerInactive(); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Player" && !collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(gameObject);
        }
    }

    protected void DestroyIfPlayerInactive()
    {
        if (player.gameObject.activeInHierarchy == false)
        {
            Destroy(gameObject);
        }
    }
}
