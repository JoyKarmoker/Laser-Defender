using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class LevelButtonManager : MonoBehaviour
{
    [SerializeField] level_loader l_loader;
    public void onButtonClick()
    {
        l_loader.LevelToLoad(Int32.Parse(EventSystem.current.currentSelectedGameObject.
            GetComponentInChildren<TextMeshProUGUI>().text));
    }
}
