using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public Door[] doors;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UnlockDoor()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].Unlock();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            UnlockDoor();
            Destroy(gameObject); 
        }
    }
}
