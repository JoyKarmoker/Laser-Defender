using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject paseMenuUI;
    [SerializeField] GameSession gameSession;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        paseMenuUI.SetActive(true);

        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        animator.Play(AllStringConstants.OPEN_DARK_PANEL_ANIM);
        gameIsPaused = true;
    }

    public void resume()
    {
        paseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        animator.Play(AllStringConstants.OPEN_IDLE_ANIM);
        animator.updateMode = AnimatorUpdateMode.Normal;
    }
    public void loadMainMenu()
    {
        LevelLoader.instance.loadSelectedLevel(1);
        Time.timeScale = 1f;
    }
    public void Restart()
    {
        LevelLoader.instance.loadSelectedLevel(SceneManager.GetActiveScene().buildIndex);
        gameSession.score = 0;
        gameSession.SetHealth(6);
        Time.timeScale = 1f;
    }
}
