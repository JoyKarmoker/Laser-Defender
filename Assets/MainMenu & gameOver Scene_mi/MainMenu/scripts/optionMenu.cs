using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class optionMenu : MonoBehaviour
{
    public AudioMixer myAudioMixer;
    public AudioMixer my_SFX_AudioMixer;

    [SerializeField] GameObject ToggleMusic;
    [SerializeField] GameObject ToggleSFX;


    float previousVolume;


    public void setMyVolume(float volume)
    {
        myAudioMixer.SetFloat("volume",volume);
    }
    public void MusicToggle()
    {

        bool isMusicToggleon = ToggleMusic.GetComponent<Toggle>().isOn;
        if(isMusicToggleon)
        {

            ToggleMusic.transform.GetChild(0).gameObject.SetActive(true);
            myAudioMixer.SetFloat("volume", previousVolume);

        }
        else
        {

            ToggleMusic.transform.GetChild(0).gameObject.SetActive(false);

            previousVolume = GetMasterLevel();

            myAudioMixer.SetFloat("volume", -80f);
            
        }
        
    }
    public float GetMasterLevel()
    {
        float value;
        bool result = myAudioMixer.GetFloat("volume", out value);
        if (result)
        {
            return value;
        }
        else
        {
            return 0f;
        }
    } 

    public void SfxToggle()
    {
        bool isSFXToggleon = ToggleSFX.GetComponent<Toggle>().isOn;
        if(isSFXToggleon)
        {
            my_SFX_AudioMixer.SetFloat("sfxVolume", 0f);
        }
        else
        {
            my_SFX_AudioMixer.SetFloat("sfxVolume", -80f);
        }
    }


    public void RateUsButton()
    {
        #if UNITY_ANDROID  
            Application.OpenURL("market://details?id=com.cseru.VirusDodger"); 

        #elif UNITY_IPHONE
            Application.OpenURL("itms-apps://itunes.apple.com/app/idYOUR_ID"); 
    
        #endif
    }
}
