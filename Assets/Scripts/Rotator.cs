using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private float rotationSpeed = 15; 

    void Update()
    {
        transform.Rotate(0, Time.deltaTime * rotationSpeed, 0); 
    }
}
