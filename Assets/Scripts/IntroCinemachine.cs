using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System;

public class IntroCinemachine : MonoBehaviour
{
    [SerializeField] GameObject portal;
    [SerializeField] GameObject cam;
    Rigidbody2D rb;
    [SerializeField] float multiplier;
    [SerializeField] float shakeFreq;
    [SerializeField] float shakeTime;
    [SerializeField] GameObject dialogue1;
    [SerializeField] DialougeManager dialougeManager;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] GameObject explosionPlanet;
    [SerializeField] Animator ConversationAnimator;
    [SerializeField] StorySceneEnemy[] planetDestroyerEnemies;
    bool isSpeedUpOn;


    bool doFade = true;

    // Start is called before the first frame update
    IEnumerator Start()
    {
       

        GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.5f;
        
        isSpeedUpOn = false;
        rb = GetComponent<Rigidbody2D>();
        AudioManager.instance.play(AllStringConstants.STORY_EARTH_SOUND, true, false);

        yield return new WaitForSeconds(3f);
        isSpeedUpOn = true;

        yield return new WaitForSeconds(1f);
        AudioManager.instance.play(AllStringConstants.STORY_DIVE_SOUND, false, true);
        AudioManager.instance.Stop(AllStringConstants.STORY_EARTH_SOUND, true);


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isSpeedUpOn)
        {

            rb.velocity = Vector2.up * multiplier;

            if (transform.position.y >= 3f && transform.position.y <=90)
            {
                multiplier += 1 + Time.deltaTime;

            }
            else if (transform.position.y > 90f && transform.position.y <= 520f)
            {
                multiplier += 1 + Time.deltaTime;

                if (doFade)
                {
                    LevelLoader.instance.StartWhiteCrossfadeTransition();
                    doFade = false;
                
                }
                //
            }
            else if (transform.position.y > 520f && transform.position.y <= 530f)
            {
                
                multiplier = 1;

                if (!doFade && transform.position.y > 527f)
                {
                    AudioManager.instance.play(AllStringConstants.STORY_MUSIC_SOUND, false, true);
                    doFade = true;
                }

            }

            else if (transform.position.y > 530f)
            {

                rb.velocity = Vector2.zero;
                multiplier = 0;
                isSpeedUpOn = false;
                StartCoroutine(SetupPortalAndPerformRestTasks());
                //Invoke("PopUpDialogue", 2.5f);
            }
        }
    }


    IEnumerator SetupPortalAndPerformRestTasks()
    {

        yield return new WaitForSeconds(1f);
        portal.SetActive(true);
        AudioManager.instance.play(AllStringConstants.STORY_PORTAL_SOUND, false, true);
        yield return new WaitForSeconds(1f);
        cam.GetComponent<RipplePostProcessor>().SetPosition(new Vector3(98.5f, 91f, 0));

        //StartCoroutine(StartExplosionSound());
        yield return new WaitForSeconds(6f);
        LevelLoader.instance.StartSpikeTransition();
        AudioManager.instance.play(AllStringConstants.STORY_SPIKETRANSITION_SOUND, false, true);
        yield return new WaitForSeconds(1f);
        cam.GetComponent<RipplePostProcessor>().enabled = false;
        GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        gameObject.transform.position = new Vector3(50, 50, transform.position.z); // planet explosion scene.. 
        foreach (var item in planetDestroyerEnemies)
        {
            item.TurnOnFiring();
        }

        explosionVFX.SetActive(true);

        yield return new WaitForSeconds(4);
        AudioManager.instance.play(AllStringConstants.STORY_BLAST_SOUND, false, true);
        CinemachineShake.Instance.ShakeCamera(shakeFreq, shakeTime);

        yield return new WaitForSeconds(0.5f);
        explosionVFX.transform.GetChild(0).gameObject.SetActive(false);
        explosionVFX.transform.GetChild(1).gameObject.SetActive(false);
        explosionPlanet.SetActive(false);

        foreach (var item in planetDestroyerEnemies)
        {
            item.TurnOffFiring();
        }

        yield return new WaitForSeconds(1f);
        portal.SetActive(false);
        LevelLoader.instance.StartSpikeTransition();
        AudioManager.instance.play(AllStringConstants.STORY_SPIKETRANSITION_SOUND, false, true);

        yield return new WaitForSeconds(1f);
        
        gameObject.transform.position = new Vector3(50, 0, transform.position.z); // start conversation scene.. 
        ConversationAnimator.Play(AllStringConstants.CONVERSATION_ANIM);

        yield return new WaitForSeconds(7f);
        PopUpDialogue();
    }

    void PopUpDialogue()
    {
        Time.timeScale = 0f;
        dialogue1.SetActive(true);
        StartCoroutine(dialougeManager.Type());
    }
}
