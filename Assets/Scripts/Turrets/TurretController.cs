using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public Transform resetTarget;

    public GameObject projectile;
    public GameObject shootPoint1;
    public GameObject shootPoint2;
    private GameObject player;

    [SerializeField] private float range = 10;
    private float resetSpeed = 3;
    [SerializeField] private float shootDelay = 1; 

    private bool isShooting = false;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        Aim();

        if (IsPlayerActive())
        {
            if (!isShooting)
            {
                StartCoroutine(WaitToShoot());
            }
        }
    }

    void Aim()
    {
        if (IsPlayerActive())
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
        else
        {
            ResetRotation();
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
                Instantiate(projectile, shootPoint1.transform.position, projectile.transform.rotation);
                Instantiate(projectile, shootPoint2.transform.position, projectile.transform.rotation);
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
        yield return new WaitForSeconds(shootDelay);
        ShootWeapon();
        isShooting = false;
    }

    float CalculateDistance()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        return distance;
    }

    bool IsPlayerActive()
    {
        return player.gameObject.activeInHierarchy;
    }
}
