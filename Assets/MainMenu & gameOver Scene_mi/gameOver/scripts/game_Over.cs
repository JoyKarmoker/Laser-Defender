using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class game_Over : MonoBehaviour
{
    public GameObject gameOver_Panel;
   

    public void gameOverPanelTweek()
    {
        gameOver_Panel.SetActive(true);
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        FindObjectOfType<GameSession>().score = 0;
        FindObjectOfType<GameSession>().health = 3;


    } public void main_menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
       

    }
}
