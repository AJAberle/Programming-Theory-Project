using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileController : MonoBehaviour
{
    private Rigidbody projectileRb;
    public GameObject forcePos;
    private Transform player;
    [SerializeField] float speed;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        projectileRb = GetComponent<Rigidbody>();
        transform.LookAt(player);
        projectileRb.AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    private void Update()
    {
        if (player.gameObject.activeInHierarchy == false)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Player" && !collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(gameObject);
        }
    }
}
