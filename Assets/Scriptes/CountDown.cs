using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class CountDown : MonoBehaviour
{
    public float timeStart = 120;
    public Text timerText;
    public LevelLoader levelLoder;
  
    // Start is called before the first frame update
    void Start()
    {
        levelLoder = FindObjectOfType<LevelLoader>();
        timerText.text = "time: " + timeStart.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        timeStart = timeStart - Time.deltaTime;
        timerText.text = "time :" + Mathf.Round(timeStart).ToString();
        if(timeStart <= 0)
        {
            SceneManager.LoadScene("win");
        }

    }
}
