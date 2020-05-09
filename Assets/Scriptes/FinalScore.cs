using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinalScore : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI finalScore;
    GameSession gameSession;
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        finalScore.text = gameSession.GetScore().ToString();
    }

    public void setScore(int score)
    {
        finalScore.text = score.ToString();
    }

}
