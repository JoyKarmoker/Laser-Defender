using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pause_Menu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject paseMenuUI;
    [SerializeField] GameSession gameSession;


     public void Pause()
    {
        Time.timeScale = 0f;
        paseMenuUI.SetActive(true);
        
        gameIsPaused = true;
    }

    public void resume()
    {
        paseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    public void loadMainMenu()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameSession.score = 0;
        gameSession.SetHealth(6);
        Time.timeScale = 1f;
    }
}
