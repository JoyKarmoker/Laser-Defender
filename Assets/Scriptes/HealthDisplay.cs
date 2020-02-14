using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] Text healthText;
    GameSession gameSession;
    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + gameSession.GetHealth().ToString();
    }
}
