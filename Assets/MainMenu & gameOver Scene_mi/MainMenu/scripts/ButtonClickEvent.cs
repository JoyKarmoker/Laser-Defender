using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickEvent : MonoBehaviour
{

    void Start()
    {
        if(GetComponent<Button>() != null)
            GetComponent<Button>().onClick.AddListener(()=> playSound());
        if(GetComponent<Toggle>() != null)
            GetComponent<Toggle>().onValueChanged.AddListener(delegate { playSound(); });
    }

    void playSound()
    {
        FindObjectOfType<audio_Manager>().play("button_sound");
    }

}
