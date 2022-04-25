using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileController : MonoBehaviour
{
    Rigidbody projectileRb;
    public GameObject forcePos;
    [SerializeField] float speed; 

    void Start()
    {
        projectileRb = GetComponent<Rigidbody>();
        projectileRb.AddForceAtPosition(transform.up * speed, forcePos.transform.position, ForceMode.Impulse); 
    }
}
