using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class LevelButtonManager : MonoBehaviour
{
    [SerializeField] LevelLoader l_loader;
    [SerializeField] Sprite[] lvlImages;
    [SerializeField] GameObject[] mapLevels;

    public void onButtonClick()
    {
        l_loader.LevelToLoad(Int32.Parse(EventSystem.current.currentSelectedGameObject.
            GetComponentInChildren<TextMeshProUGUI>().text));
    }

    #region Map Level Management

    #region Map Data Management
    public struct MapData
    {
        public bool isLevelUnlock, isPlayed;
        public int starValue;
    }
    public MapData mapData;
    /// <summary>
    /// its is called when user clicks the level button..
    /// </summary>


    void SaveMapInfo(int lvl, bool isLevelUnlock, bool isPlayed, int starValue)
    {
        ES3.Save<bool>("Level" + lvl + "Unlock", isLevelUnlock);
        ES3.Save<bool>("Level" + lvl + "Played", isPlayed);
        ES3.Save<int>("Level" + lvl + "Star", starValue);
    }
    MapData LoadMapInfo(int lvl)
    {
        MapData mapData = new MapData
        {
            isLevelUnlock = ES3.Load<bool>("Level" + lvl + "Unlock", false),
            isPlayed = ES3.Load<bool>("Level" + lvl + "Played", false),
            starValue = ES3.Load<int>("Level" + lvl + "Star", 0)

        };
        return mapData;
    } 
    #endregion

    public void UpdateMap()
    {
        bool updateMarker = true;

        //set level 1 data to default if story hasn't seen yet..
        if (!ES3.Load<bool>(AllStringConstants.HAS_PLAYER_WATCHED_STORY, false))
        {
            SaveMapInfo(1, true, false, 0);
        }

        for (int i = mapLevels.Length - 1; i >=0 ; i--)
        {
            mapData = LoadMapInfo(i + 1);

            //disable particle effect..
            mapLevels[i].transform.GetChild(1).gameObject.SetActive(false);

            //check if unlock..
            if (mapData.isLevelUnlock)
            {
                if(updateMarker)
                {
                    //enable particle effect..
                    mapLevels[i].transform.GetChild(1).gameObject.SetActive(true);

                    updateMarker = false;
                }
                
                //activate button
                mapLevels[i].GetComponent<Button>().interactable = true;

                //check for played or not..
                if (mapData.isPlayed)
                {
                    //sets button sprite accoording to valid sprite data.. 
                    mapLevels[i].GetComponent<Image>().sprite = lvlImages[mapData.starValue + 1]; // start value is between 1 and 3.
                }
                else
                {
                    //set button sprite to not played but unlocked..
                    mapLevels[i].GetComponent<Image>().sprite = lvlImages[1];
                }
            }
            else
            {
                //dectivate button sprite..
                mapLevels[i].GetComponent<Image>().sprite = lvlImages[0];

                //deactivate button..
                mapLevels[i].GetComponent<Button>().interactable = false;
            }
        }
    } 
    #endregion




}
