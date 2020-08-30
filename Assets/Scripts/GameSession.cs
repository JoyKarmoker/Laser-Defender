using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{

    [SerializeField] GameObject[] healthBars;
    [SerializeField] FinalScore finalScore;
    public int score = 0;
    int health = 5;


    private void Awake()
    {
       
        SetUpSingleTon();
        
    }

    private void Start()
    {
        foreach (var item in healthBars)
        {
            item.SetActive(true);
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



}
