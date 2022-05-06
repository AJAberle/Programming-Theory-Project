using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int score = 0; 
    private int lives = 1;

    public static GameManager Instance { get; private set; }

    public GameObject deathMenu;
    public GameObject gameOverMenu;
    public GameObject shieldIcon;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText; 
    public bool isShieldActivated { get; private set; }

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
        UpdateText();
    }

    public void PlayerDeath()
    {
        if (lives > 0)
        {
            AddLives(-1); 
            gameOverMenu.SetActive(false); 
            deathMenu.SetActive(true);
        }
        else
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerController.startPos = playerController.originalPos; 
            deathMenu.SetActive(false); 
            gameOverMenu.SetActive(true); 
        }
    }

    public void RespawnPlayer()
    {
        player.SetActive(true);
        deathMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        player.GetComponent<PlayerController>().Respawn();
        //hello my Bro!!!:D
    }

    public void ActivateShield()
    {
        if (!isShieldActivated)
        {
            isShieldActivated = true;
            shieldIcon.SetActive(true);
        }
    }

    public void DeactivateShield()
    {
        isShieldActivated = false;
        shieldIcon.SetActive(false);
    }

    public void AddLives(int livesToAdd)
    {
        lives += livesToAdd;
        UpdateText(); 
    }

    public void AddScore(int pointsToAdd)
    {
        score += pointsToAdd;
        UpdateText(); 
    }

    private void UpdateText()
    {
        livesText.SetText($"Lives: {lives}");
        scoreText.SetText($"Score: {score}"); 
    }
}
