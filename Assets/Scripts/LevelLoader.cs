using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance { get; private set; }

    AudioManager m_audioManager;

    public Animator crossfadeAnimatorBlack;
    public Animator crossfadeAnimatorWhite;
    public Animator spikeAnimator;
    float transitionTime = 2f;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        m_audioManager = AudioManager.instance;
    }

    //loads selected level with fade anim
    public void loadSelectedLevel(int sceneIndex, bool fadeout)
    {
        StartCoroutine(Load_Level(sceneIndex, fadeout));
    }

    IEnumerator Load_Level(int sceneIndex, bool fadeout)
    {
        crossfadeAnimatorBlack.SetTrigger(AllStringConstants.CROSSFADE_BLACK);

        if (fadeout)
        {
            StartCoroutine(AudioManager.StartFade(m_audioManager.mainMixture, AllStringConstants.MASTER_AUDIOMIXER, transitionTime - 0.5f, -80f));
            StartCoroutine(AudioManager.StartFade(m_audioManager.sfxMixture, AllStringConstants.SFX_AUDIOMIXER, transitionTime - 0.5f, -80f));
        }

        yield return new WaitForSeconds(transitionTime);

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            m_audioManager.StopAllAudio();
        }

        SceneManager.LoadScene(sceneIndex);

    }
    //loads lvl from map menu with fade
    public  void LevelToLoad(int index)
    {
        string lvlName = "Lvl" + index;
        if(index == 1 && PlayerPrefs.GetInt(lvlName,0) == 0)
            StartCoroutine(Load_Level(index + 1, true));
        else
            StartCoroutine(Load_Level(index+2, true));
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
