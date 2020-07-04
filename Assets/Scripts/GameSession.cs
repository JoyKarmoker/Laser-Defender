using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{


    public int score = 0;
    public int health = 3;


    private void Awake()
    {
       
        SetUpSingleTon();
        
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

        FindObjectOfType<FinalScore>().setScore(score);

    }

    public int GetHealth()
    {
        FindObjectOfType<HealthBar>().setHealth(health);
        return health;
    }

    public void DecreaseHealth()
    {
        health = health - 1;
       
    }
    public void ResetGame()
    {
        Destroy(gameObject);
    }



}
