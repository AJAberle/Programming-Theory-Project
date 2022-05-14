using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    private int score = 0;
    private int lives = 5;
    private int startLives;

    private bool isPaused = false;

    public static GameManager Instance { get; private set; }

    [Header("GameObjects")]
    public GameObject pauseMenu;
    public GameObject deathMenu;
    public GameObject gameOverMenu;
    public GameObject winMenu;
    public GameObject shieldIcon;
    private GameObject player;

    [Header("Text")]
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;

    private KeyCode pauseKey = KeyCode.P;

    public bool isShieldActivated { get; private set; }


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
        startLives = lives;
        UpdateText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (isPaused)
        {
            isPaused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            isPaused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
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
        winMenu.SetActive(false);
        player.GetComponent<PlayerController>().Respawn();
        //hello my Bro!!!:D
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneLoader.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneLoader.Instance.LoadScene(0);
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); 
#endif
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

    public void Win()
    {
        Debug.Log("<color=green>You win!</color>");
        deathMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        winMenu.SetActive(true);
        player.GetComponent<PlayerController>().canMove = false;
    }

    public void Checkpoint()
    {
        player.GetComponent<PlayerController>().Checkpoint();
    }
}
