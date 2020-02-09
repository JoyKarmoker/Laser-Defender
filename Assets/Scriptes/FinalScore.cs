using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
    Text finalScore;
    GameSession gameSession;
    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        finalScore = FindObjectOfType<Text>();
        finalScore.text = gameSession.GetScore().ToString();
    }

}
