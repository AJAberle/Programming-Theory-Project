using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    private GameObject player;
    private GameObject currentTarget; 
    private float range = 15; 

    private void Start()
    {
        player = GameObject.Find("Player"); 
    }

    private void Update()
    {
        AimAtPlayer(); 
    }

    void AimAtPlayer()
    {
        RaycastHit hit; 
        if(Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if(hit.transform.gameObject.name != "Ground")
            {
                transform.LookAt(player.transform);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0); 
            }
        }
    }
}
