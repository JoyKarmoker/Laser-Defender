using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class game_Over : MonoBehaviour
{
    public GameObject gameOver_Panel;
   
    // this method is called when game is over
    public void gameOverPanelTweek()
    {
        gameOver_Panel.SetActive(true);
    }

}
