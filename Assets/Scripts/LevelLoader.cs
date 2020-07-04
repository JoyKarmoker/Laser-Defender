using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
     float delayInSeconds = 2f;
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(2);
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadLooseScene()
    {
        StartCoroutine(WaitAndLoadLoose());
        
    }


    public void LoadWinScene()
    {
        StartCoroutine(WaitAndLoadWin());
    }
    IEnumerator WaitAndLoadLoose()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Loose");
    }

    IEnumerator WaitAndLoadWin()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("win");
    }
    public void QuitGame()
    {
        Application.Quit();
    }




}
