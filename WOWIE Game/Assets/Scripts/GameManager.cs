using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pausedSystem;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private Health printerHealth;


    private bool isPaused;
    private bool gameOver;

    void Awake()
    {
        SetTimeScale(1.0f);
    }

    private void OnEnable()
    {
        printerHealth.OnHit += OnHit;
    }

    private void OnDisable()
    {
        printerHealth.OnHit -= OnHit;
    }


    void Update()
    {
        if(pausedSystem == null) return;

        if (gameOver) return;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPaused)
            {
                isPaused = true;
                pausedSystem.SetActive(true);
                SetTimeScale(0.0f);
            }
            else if(isPaused)
            {
                isPaused = false;
                pausedSystem.SetActive(false);
                SetTimeScale(1.0f);
            }
        }
    }

    public void ShowDeathScreen()
    {
        gameOver = true;
        deathScreen.SetActive(true);
    }

    void OnHit(float healthPercent, bool isHit)
    {
        if(healthPercent < 0.0f)
        {
            ShowDeathScreen();
        }
    }

    private void SetTimeScale(float value)
    {
        Time.timeScale = value;
    }

    public void ResumeGame()
    {
        SetTimeScale(1.0f);
        pausedSystem.SetActive(false);
    }

    public void GoToMainScene()
    {
        SceneManager.LoadScene("MAIN");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
