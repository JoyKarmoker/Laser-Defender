using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{

    [SerializeField] GameObject[] healthBars;
    [SerializeField] FinalScore finalScore;
    public int score = 0;
    int health = 6;


    private void Awake()
    {
       
        SetUpSingleTon();
        
    }

    private void Start()
    {
        if (healthBars != null)
        {
            foreach (var item in healthBars)
            {
                item.SetActive(true);
            }
        }
    }
    private void SetUpSingleTon()
    {
        int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreValue)
    {
        score = score + scoreValue;

        finalScore.setScore(score);

    }

    public int GetHealth()
    {
        return health;
    }

    public void SetHealth(int health)
    {
        this.health = health;
        for (int i = 0; i < health; i++)
        {
            healthBars[i].SetActive(true);
        }
    }

    public void DecreaseHealth()
    {
        if(health > 0 )
            healthBars[--health].SetActive(false);     
        else
            healthBars[health].SetActive(false);

    }
    public void ResetGame()
    {
        Destroy(gameObject);
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ES3.Save("myInt", score);
            ES3.Save("myfloat", 12.3f);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            int s = ES3.Load<int>("myInt",0);
            float f = ES3.Load<float>("myfloat", 0);

            Debug.LogError(s);
            Debug.LogError(f);
        }

        if (Input.GetKeyDown(KeyCode.R))
            ES3.DeleteKey("myInt");

        if (Input.GetKeyDown(KeyCode.D))
            ES3.DeleteFile("Glitch.xo");

        if (Input.GetKeyDown(KeyCode.P))
            score += 50;
    }*/


}
