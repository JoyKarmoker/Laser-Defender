using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionMenu : MonoBehaviour
{
    public AudioMixer myAudioMixer;
    public AudioMixer my_SFX_AudioMixer;

    [SerializeField] GameObject ToggleMusic;
    [SerializeField] GameObject ToggleSFX;
    [SerializeField] GameObject ToggleVBR;


    float previousVolume;


    public void setMyVolume(float volume)
    {
        myAudioMixer.SetFloat(AllStringConstants.MASTER_VOLUME_MIXER,volume);
    }
    public void MusicToggle()
    {

        bool isMusicToggleon = ToggleMusic.GetComponent<Toggle>().isOn;
        if(isMusicToggleon)
        {

            ToggleMusic.transform.GetChild(0).gameObject.SetActive(true);
            myAudioMixer.SetFloat(AllStringConstants.MASTER_VOLUME_MIXER, previousVolume);

        }
        else
        {

            ToggleMusic.transform.GetChild(0).gameObject.SetActive(false);

            previousVolume = GetMasterLevel();

            myAudioMixer.SetFloat(AllStringConstants.MASTER_VOLUME_MIXER, -80f);
            
        }
        
    }
    public float GetMasterLevel()
    {
        float value;
        bool result = myAudioMixer.GetFloat(AllStringConstants.MASTER_VOLUME_MIXER, out value);
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
            my_SFX_AudioMixer.SetFloat(AllStringConstants.SFX_VOLUME_MIXER, 0f);
        }
        else
        {
            my_SFX_AudioMixer.SetFloat(AllStringConstants.SFX_VOLUME_MIXER, -80f);
        }
    } 
    public void VBRToggle()
    {
        bool isVBRToggleon = ToggleVBR.GetComponent<Toggle>().isOn;

        if(isVBRToggleon)
        {
            ES3.Save<bool>(AllStringConstants.VIBRATION_STATUS, true);
        }
        else
        {
            ES3.Save<bool>(AllStringConstants.VIBRATION_STATUS, false);
        }
    }


    public void RateUsButton()
    {
        #if UNITY_ANDROID  
            Application.OpenURL(AllStringConstants.RATE_US_INFO_ANDROID);

#elif UNITY_IPHONE
            Application.OpenURL(AllStringConstants.RATE_US_INFO_IOS); 
    
#endif
    }
}
