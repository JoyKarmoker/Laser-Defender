using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance { get; private set; }

    public Animator crossfadeAnimatorBlack;
    public Animator crossfadeAnimatorWhite;
    public Animator spikeAnimator;
    public float transitionTime = 1f;
    private void Awake()
    {
        instance = this;
    }

    //loads next level with fade anim
    public void loadNextLevel()
    {
        StartCoroutine(Load_Level(SceneManager.GetActiveScene().buildIndex + 1));
    }
    //loads selected level with fade anim
    public void loadSelectedLevel(int sceneIndex)
    {
        StartCoroutine(Load_Level(sceneIndex));
    }

    IEnumerator Load_Level(int sceneIndex)
    {        
        crossfadeAnimatorBlack.SetTrigger(AllStringConstants.CROSSFADE_BLACK);
     

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneIndex);

    }
    //loads lvl from map menu with fade
    public  void LevelToLoad(int index)
    {
        string lvlName = "Lvl" + index;
        if(index == 1 && PlayerPrefs.GetInt(lvlName,0) == 0)
            StartCoroutine(Load_Level(index + 1));
        else
            StartCoroutine(Load_Level(index+2));
    }

    public void StartSpikeTransition()
    {
        spikeAnimator.SetTrigger(AllStringConstants.SPIKE_TRANSITION_START);
    }

    public void StartWhiteCrossfadeTransition()
    {
        crossfadeAnimatorWhite.SetTrigger(AllStringConstants.CROSSFADE_WHITE);
    }

}
