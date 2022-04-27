using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public GameObject explosion; 

    private Rigidbody playerRb;
    private float yBound = -10;

    public Vector3 startPos { get; private set; }

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        startPos = transform.position; 
    }

    void Update()
    {
        HandleInput(); 

        if(transform.position.y < yBound)
        {
            Die(); 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
            Die(); 
        }
    }

    void HandleInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementVector = new Vector3(horizontalInput, 0, verticalInput);

        playerRb.AddForce(movementVector); 
    }

    public void Respawn()
    {
        playerRb.velocity = Vector3.zero;
        playerRb.angularVelocity = Vector3.zero; 
        transform.position = startPos; 
    }

    void Die()
    {
        Instantiate(explosion, transform.position, explosion.transform.rotation);
        GameManager.Instance.PlayerDeath();
        gameObject.SetActive(false);
    }
}
