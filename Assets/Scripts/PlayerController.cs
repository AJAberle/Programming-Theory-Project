using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public GameObject explosion;

    private Rigidbody playerRb;
    private float yBound = -10;
    private float speed = 1.5f;
    private bool isDead = false;
    private KeyCode jumpKey = KeyCode.Space;
    private bool canJump = false;
    private float jumpForce = 5;

    public Vector3 startPos { get; private set; }

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        startPos = transform.position;
    }

    void Update()
    {
        HandleInput();

        if (transform.position.y < yBound)
        {
            Die();
        }

        if (Input.GetKeyDown(jumpKey))
        {
            Jump();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            if (!isDead)
            {
                Destroy(collision.gameObject);
                Die();
            }
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Explosion"))
        {
            if (!isDead)
            {
                Die();
            }
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

    void Jump()
    {
        if (canJump)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
        }
    }
}
