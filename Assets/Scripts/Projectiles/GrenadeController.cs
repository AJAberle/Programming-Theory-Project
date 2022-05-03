using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    public GameObject explosion;
    public GameObject explosionRange; 

    private float explosionDelay = 2; 
    private float force = 7.5f; 
    private GameObject player;
    private Rigidbody grenadeRb;
    private Material i_material; 

    private void Start()
    {
        grenadeRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        i_material = GetComponent<Renderer>().material;

        transform.LookAt(player.transform);
        grenadeRb.AddForce(transform.forward * force, ForceMode.Impulse);

        StartCoroutine(WaitToExplode()); 
    }

    private void Update()
    {
        i_material.color += new Color(0.005f, 0, 0); 
    }

    IEnumerator WaitToExplode()
    {
        yield return new WaitForSeconds(explosionDelay);
        Explode();  
    }

    void Explode()
    {
        Instantiate(explosion, transform.position, explosion.transform.rotation);
        Instantiate(explosionRange, transform.position, explosionRange.transform.rotation);
        Destroy(gameObject); 
    }

    private void OnDestroy()
    {
        Destroy(i_material); 
    }
}
