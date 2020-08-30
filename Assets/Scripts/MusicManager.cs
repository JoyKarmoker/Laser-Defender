using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] musicChangeArray;
    private AudioSource audioSource;
    
    // Start is called before the first frame update

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Debug.Log("Don't Destroy On load " + name);
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
       
    }

    private void OnLevelWasLoaded(int level)
    {
        AudioClip thisLevelMusic = musicChangeArray[level];
        Debug.Log("Playing AudioClip: " + thisLevelMusic);
        if(thisLevelMusic)
        {
            audioSource.clip = thisLevelMusic;
            audioSource.loop = true;
            audioSource.Play();
        }

    }
}
