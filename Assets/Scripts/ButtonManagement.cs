using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagement : MonoBehaviour
{
    [SerializeField] GameObject map;
    public void ExitButton()
    {
        Application.Quit();
    }

    public void Mapclose()
    {
        gameObject.SetActive(false);
    }

}
