using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagement : MonoBehaviour
{
  
    public void ExitButton()
    {
        Application.Quit();
    }

    public void normalButton()
    {
        FindObjectOfType<level_loader>().loadNextLevel();
    }
    public void reverseButton()
    {
        FindObjectOfType<level_loader>().loadNextLevel();
    }
    
}
