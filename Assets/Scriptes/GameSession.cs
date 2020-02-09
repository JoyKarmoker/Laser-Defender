using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;
    int health = 3;
    

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
    }

    public int GetHealth()
    {
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
