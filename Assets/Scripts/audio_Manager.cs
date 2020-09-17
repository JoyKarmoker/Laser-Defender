using UnityEngine.Audio;
using UnityEngine;
using System.Collections;
using System;

public class audio_Manager : MonoBehaviour
{
    public Sounds[] sounds;

    //public AudioClip gameOverClip;

    public static audio_Manager instance;
    static bool keepFadingIn;
    static bool keepFadingOut;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.outputAudioMixerGroup = s.masterMixtureGroup;
            s.source.pitch = s.pinch;
            s.source.loop = s.loop;
        }
    }

   /* private void Start()
    {
        play("bg_sound");
        sounds[0].source.loop = true; // bg sound
    }*/

    public void play(string name, bool fadeIn)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if(s==null)
        {
            Debug.LogWarning("sound: " + name + " not found");
            return;
        }
        s.source.Play();

        if (fadeIn)
            StartCoroutine(FadeInAudio(s));
    }

    public static IEnumerator FadeInAudio(Sounds track)
    {
        keepFadingIn = true;
        keepFadingOut = false;
        track.source.volume = 0;
        float audioVolume = track.source.volume;

        while(track.source.volume <= track.volume && keepFadingIn)
        {
            audioVolume += 0.001f;
            track.source.volume = audioVolume;
            yield return new WaitForSeconds(0.1f);
        }
        
    }
    
    /*public static IEnumerator FadeOutAudio()
    {
        keepFadingIn = false;
        keepFadingOut = true;

        float audioVolume = track.source.volume;

        while (track.source.volume >= 1 && keepFadingOut)
        {
            audioVolume -= 0.01f;
            track.source.volume = audioVolume;
            yield return new WaitForSeconds(0.1f);
        }
    }*/

}
