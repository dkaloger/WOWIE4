using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pausedSystem;
    private bool isPaused;

    void Awake()
    {
        SetTimeScale(1.0f);
    }


    void Update()
    {
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

    private void SetTimeScale(float value)
    {
        Time.timeScale = value;
    }

    public void ResumeGame()
    {
        SetTimeScale(1.0f);
        pausedSystem.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
