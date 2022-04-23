using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private float yBound = -10;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();

        startPos = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput(); 

        if(transform.position.y < yBound)
        {
            transform.position = startPos;
        }
    }

    void HandleInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementVector = new Vector3(horizontalInput, 0, verticalInput).normalized;

        playerRb.AddForce(movementVector); 
    }
}
