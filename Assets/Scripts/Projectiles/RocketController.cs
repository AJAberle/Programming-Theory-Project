using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : ProjectileController
{
    public override void Update()
    {
        DestroyIfPlayerInactive();
        transform.LookAt(player);
        projectileRb.AddForce(transform.forward * speed, ForceMode.Force); 
    }
}
