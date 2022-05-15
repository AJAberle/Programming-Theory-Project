using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class RocketController : ProjectileController
{
    private float accelerationSpeed = 0.5f;
    private float followTime = 1.5f;
    private bool isFollowingPlayer = true;

    protected override void Start()
    {
        isFollowingPlayer = true; 
        Initialize();
        StartCoroutine(DisableFollowPlayer()); 
    }

    // POLYMORPHISM
    protected override void Update()
    {
        DestroyIfPlayerInactive();

        if (isFollowingPlayer && playerObject.activeInHierarchy)
        {
            transform.LookAt(playerObject.transform);
            projectileRb.velocity = transform.forward * speed;
        }
        else
        {
            projectileRb.AddForce(transform.forward * accelerationSpeed); 
            projectileRb.angularVelocity = new Vector3(0, 0, 0); 
        }

    }

    IEnumerator DisableFollowPlayer()
    {
        yield return new WaitForSeconds(followTime);
        isFollowingPlayer = false; 
    }

}
