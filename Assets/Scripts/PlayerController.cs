using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public GameObject explosion;
    public GameObject collectFX;

    private Rigidbody playerRb;
    private float yBound = -10;
    private float speed = 15f;
    private bool isDead = false;
    private KeyCode jumpKey = KeyCode.Space;
    private bool canJump = false;
    private float jumpForce = 5;
    private bool canDie = true;

    private int collisionTimes = 0;

    public Vector3 startPos { get; set; }
    public Vector3 originalPos { get; private set; }

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        startPos = transform.position;
        originalPos = startPos;
    }

    void Update()
    {
        if (transform.position.y < yBound)
        {
            Die();
        }

        if (Input.GetKeyDown(jumpKey))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        HandleInput();
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

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.CompareTag("ShieldPowerup"))
        {
            InstantiateCollectEffect();
            GameManager.Instance.ActivateShield();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Life"))
        {
            collisionTimes = 0;
            InstantiateCollectEffect();
            GameManager.Instance.AddLives(1);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Gem1"))
        {
            InstantiateCollectEffect();
            GameManager.Instance.AddScore(1);
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Gem2"))
        {
            InstantiateCollectEffect();
            GameManager.Instance.AddScore(2);
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Explosion"))
        {
            collisionTimes = 0;
        }

    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("Explosion"))
        {
            collisionTimes++;
        }

        if (collision.gameObject.CompareTag("Explosion") && collisionTimes > 2)
        {

            if (!isDead && canDie)
            {
                collisionTimes = 0;
                Die();
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Explosion"))
        {
            if (!GameManager.Instance.isShieldActivated)
            {
                canDie = true;
            }
        }
    }

    void HandleInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float maxSpeed = 12.5f;

        Vector3 movementVector = new Vector3(horizontalInput * speed, 0, verticalInput * speed);

        playerRb.AddForce(movementVector, ForceMode.Force);

        if (playerRb.velocity.magnitude > maxSpeed)
        {
            playerRb.velocity = playerRb.velocity.normalized * maxSpeed;
        }
    }

    public void Respawn()
    {
        canDie = true;
        isDead = false;
        playerRb.velocity = Vector3.zero;
        playerRb.angularVelocity = Vector3.zero;
        transform.position = startPos;
    }

    void Die()
    {
        if (!GameManager.Instance.isShieldActivated)
        {
            isDead = true;
            explosion.transform.position = transform.position;
            explosion.gameObject.SetActive(true);
            GameManager.Instance.PlayerDeath();
            gameObject.SetActive(false);
        }
        else
        {
            canDie = false;
            GameManager.Instance.DeactivateShield();
        }
    }

    void Jump()
    {
        if (canJump)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
        }
    }

    public void InstantiateCollectEffect()
    {
        Instantiate(collectFX, transform.position, collectFX.transform.rotation);
    }

    public void Checkpoint()
    {
        startPos = transform.position;
    }
}
