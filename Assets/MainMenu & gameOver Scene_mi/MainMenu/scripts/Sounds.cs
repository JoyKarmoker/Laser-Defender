using UnityEngine.Audio;
using System;
using UnityEngine;

[System.Serializable]
public class Sounds
{
    
    public string name;

    public AudioClip clip;
    public AudioMixerGroup masterMixtureGroup;

    [Range(0f,1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pinch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

}
