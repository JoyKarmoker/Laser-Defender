using UnityEngine.Audio;
using UnityEngine;
using System.Collections;
using System;

public class AudioManager : MonoBehaviour
{
    [HideInInspector]
    public bool isNotMainMenuLoadedForFirstTime = false;

    public Sounds[] sounds;
    public AudioClip[] levelBGClips;
    public AudioClip[] menuBGClips;

    public AudioMixer mainMixture; 
    public AudioMixer sfxMixture;

    public static AudioManager instance;
    static bool keepFadingIn;
    static bool keepFadingOut;
    Coroutine disableSource_Routine;

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
            s.source.playOnAwake = false;
            s.source.enabled = false;
        }
    }

    public void play(string name, bool fadeIn, bool autoTurnOff)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);

        if(s==null)
        {
            Debug.LogWarning("sound: " + name + " not found");
            return;
        }

        s.source.enabled = true;
        s.source.Play();

        if (!s.source.loop && autoTurnOff)
        {
            float soundLength = s.source.clip.length + 5f;

            if (disableSource_Routine != null)
                StopCoroutine(disableSource_Routine);

            disableSource_Routine = StartCoroutine(DisableAudioSource(s.source, soundLength));
        }

        if (fadeIn)
            StartCoroutine(FadeInAudio(s));
    }


    IEnumerator DisableAudioSource(AudioSource source, float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        source.enabled = false;

    }
    public static IEnumerator FadeInAudio(Sounds track)
    {
        keepFadingIn = true;
        keepFadingOut = false;
        track.source.volume = 0;
        float audioVolume = track.source.volume;

        while (track.source.volume <= track.volume && keepFadingIn)
        {
            audioVolume += 0.03f;
            track.source.volume = audioVolume;
            yield return new WaitForSeconds(0.1f);
        }

    }


    public void Stop(string name, bool fadeout)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("sound: " + name + " not found");
            return;
        }

        if (fadeout)
            StartCoroutine(FadeOutAudio(s));
        else
        {
            s.source.Stop();
            s.source.enabled = false;
        }
    }





    public static IEnumerator FadeOutAudio(Sounds track)
    {
        keepFadingIn = false;
        keepFadingOut = true;

        float audioVolume = track.source.volume;

        while (track.source.volume >= 0 && keepFadingOut)
        {
            audioVolume -= 0.05f;
            track.source.volume = audioVolume;
            yield return new WaitForSeconds(0.1f);
        }

        track.source.Stop();
        track.source.enabled = false;
    }


    public void FindAndReplaceAudioClip(string name, AudioClip clip)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("sound: " + name + " not found");
            return;
        }

        s.source.clip = clip;
    }


    public static IEnumerator StartFade(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
    {
        float currentTime = 0;
        float currentVol;
        audioMixer.GetFloat(exposedParam, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            yield return null;
        }
        yield break;
    }

    /// <summary>
    /// calls when new scene loads but only on demand..
    /// </summary>
    public void StopAllAudio()
    {
        foreach (Sounds s in sounds)
        {
            s.source.Stop();
            s.source.enabled = false;
        }
    }

}
