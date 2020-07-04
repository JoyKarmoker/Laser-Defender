using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level_loader : MonoBehaviour
{
    public Animator my_animator;
    public float transitionTime = 1f;

    [SerializeField] private float Audio_delay = 1f;


    public void loadNextLevel()
    {
        StartCoroutine(Load_Level(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator Load_Level(int sceneIndex)
    {
        float elapsedTime = 0;
        float currentVolume = AudioListener.volume;
        
        my_animator.SetTrigger("fade");

        while (elapsedTime < Audio_delay)
        {
            elapsedTime += Time.deltaTime;
            AudioListener.volume = Mathf.Lerp(currentVolume, 0, elapsedTime / Audio_delay);
        }
        

        yield return new WaitForSeconds(Audio_delay);

        SceneManager.LoadScene(sceneIndex);
        AudioListener.volume = currentVolume;

    }
}
