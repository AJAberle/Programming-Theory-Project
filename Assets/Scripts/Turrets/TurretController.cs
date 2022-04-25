using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public Transform resetTarget;

    public GameObject projectile;
    private GameObject player;

    [SerializeField] private float range = 10;
    private float resetSpeed = 3;

    private bool isShooting = false; 

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        Aim();

        if(!isShooting)
        {
            StartCoroutine(WaitToShoot()); 
        }
    }

    void Aim()
    {
        if (CalculateDistance() > range)
        {
            ResetRotation();
        }
        else
        {
            LookAtPlayer();
        }
    }

    void ResetRotation()
    {
        Vector3 targetDirection = resetTarget.position - transform.position;
        float singleStep = resetSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    void LookAtPlayer()
    {
        transform.LookAt(player.transform);
    }

    void ShootWeapon()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (!hit.transform.gameObject.CompareTag("Obstacle"))
            {
                Debug.Log("Shooting");
                Instantiate(projectile, new Vector3(0, 1, 0), projectile.transform.rotation); 
            }
            else
            {
                Debug.Log("Can't shoot");
            }
        }
    }

    IEnumerator WaitToShoot()
    {
        isShooting = true;
        yield return new WaitForSeconds(1);
        ShootWeapon();
        isShooting = false; 
    }

    float CalculateDistance()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        return distance; 
    }
}
