using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SplashCounter : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeStart = 10;
    public TextMeshProUGUI timerText;
    public LevelLoader levelLoder;


    // Start is called before the first frame update
    void Start()
    {
        levelLoder = FindObjectOfType<LevelLoader>();
        timerText.text = timeStart.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        timeStart = timeStart - Time.deltaTime;
        timerText.text = Mathf.Round(timeStart).ToString();
        if (timeStart <= 0)
        {
            levelLoder.LoadStartMenu();
        }

    }
}
