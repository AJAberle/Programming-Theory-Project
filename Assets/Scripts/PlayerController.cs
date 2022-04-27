using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public GameObject explosion; 

    private Rigidbody playerRb;
    private float yBound = -10;
    private float speed = 2;
    private bool isDead = false; 

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
        if(collision.gameObject.CompareTag("Projectile") && !isDead)
        {
            Destroy(collision.gameObject);
            Die(); 
        }
    }

    void HandleInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementVector = new Vector3(horizontalInput * speed, 0, verticalInput * speed);

        playerRb.AddForce(movementVector, ForceMode.Force); 
    }

    public void Respawn()
    {
        isDead = false;
        playerRb.velocity = Vector3.zero;
        playerRb.angularVelocity = Vector3.zero;
        transform.position = startPos; 
    }

    void Die()
    {
        isDead = true;
        explosion.transform.position = transform.position;
        explosion.gameObject.SetActive(true); 
        GameManager.Instance.PlayerDeath();
        gameObject.SetActive(false);
    }
}
