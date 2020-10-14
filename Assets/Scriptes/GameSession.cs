using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{


    public int score = 0;
    int health = 6;


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

    public void SetHealth(int health)
    {
        this.health = health;
      
    }

    public void DecreaseHealth()
    {
        if (health > 0)
            --health;

    }
    public void ResetGame()
    {
        Destroy(gameObject);
    }



}
