using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class LevelButtonManager : MonoBehaviour
{
    [SerializeField] level_loader l_loader;
    [SerializeField] Sprite[] lvlImages;
    [SerializeField] GameObject[] mapLevels;
    public void onButtonClick()
    {
        l_loader.LevelToLoad(Int32.Parse(EventSystem.current.currentSelectedGameObject.
            GetComponentInChildren<TextMeshProUGUI>().text));
    }
}
