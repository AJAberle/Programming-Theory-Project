using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public Door[] doors;
    private PlayerController player; 
    private float rotationSpeed = 15;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>(); 
    }

    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    public void UnlockDoor()
    {
        player.InstantiateCollectEffect(); 
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].Unlock();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            UnlockDoor();
            gameObject.SetActive(false); 
        }
    }
}
