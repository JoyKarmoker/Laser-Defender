using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioHandler : MonoBehaviour
{
    AudioManager m_audioManager;
    // Start is called before the first frame update
    void Start()
    {
        SetupAudio();
    }

    public void SetupAudio()
    {
        m_audioManager = AudioManager.instance;

        //get the clips
        AudioClip clipForMenuBg = m_audioManager.menuBGClips[Random.Range(0, m_audioManager.menuBGClips.Length)];
        AudioClip clipForLevelBg = m_audioManager.levelBGClips[Random.Range(0, m_audioManager.levelBGClips.Length)];

        //check for scene index to do specific tasks..
        if (SceneManager.GetActiveScene().buildIndex == 1 && AudioManager.instance.isNotMainMenuLoadedForFirstTime)
        {
            m_audioManager.FindAndReplaceAudioClip(AllStringConstants.BG_SOUND, clipForMenuBg);

            FadeAndPlay();
        }
        else if (SceneManager.GetActiveScene().buildIndex > 2)
        {
            m_audioManager.FindAndReplaceAudioClip(AllStringConstants.GAMELEVEL_SOUND, clipForLevelBg);

            FadeAndPlay();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            //turn the volume to 1 again..
            StartCoroutine(AudioManager.StartFade(m_audioManager.mainMixture, AllStringConstants.MASTER_AUDIOMIXER, 1, 1));
            StartCoroutine(AudioManager.StartFade(m_audioManager.sfxMixture, AllStringConstants.SFX_AUDIOMIXER, 1, 1));
        }


    }

    private void FadeAndPlay()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            //play the menu bg sound.. 
            m_audioManager.play(AllStringConstants.BG_SOUND, false, false);
        }
        else
        {
            //play the level bg sound.. 
            m_audioManager.play(AllStringConstants.GAMELEVEL_SOUND, false, false);
        }

        //turn the volume to 1 again..
        StartCoroutine(AudioManager.StartFade(m_audioManager.mainMixture, AllStringConstants.MASTER_AUDIOMIXER, 1, 1));
        StartCoroutine(AudioManager.StartFade(m_audioManager.sfxMixture, AllStringConstants.SFX_AUDIOMIXER, 1, 1));
    }
}
