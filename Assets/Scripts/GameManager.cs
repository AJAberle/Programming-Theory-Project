using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject deathMenu; 

    private GameObject player;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); 
            return; 
        }

        Instance = this;
    }

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void PlayerDeath()
    {
        deathMenu.SetActive(true); 
    }

    public void RespawnPlayer()
    {
        player.SetActive(true);
        deathMenu.SetActive(false);
        player.GetComponent<PlayerController>().Respawn(); 
        //hello my Bro!!!:D
    }
}
