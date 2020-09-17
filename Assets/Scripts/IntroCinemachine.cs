using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System;

public class IntroCinemachine : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float multiplier;
    [SerializeField] GameObject dialogue1;
    [SerializeField] DialougeManager dialougeManager;
    bool isSpeedUpOn;
    CinemachineStoryboard cinemachineStoryboard;



    bool doFade = true;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        cinemachineStoryboard = GetComponent<CinemachineStoryboard>();
        GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0.5f;
        
        isSpeedUpOn = false;
        rb = GetComponent<Rigidbody2D>();

        yield return new WaitForSeconds(3f);
        isSpeedUpOn = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isSpeedUpOn)
        {

            rb.velocity = Vector2.up * multiplier;

            if (transform.position.y >= 3f)
            {
                multiplier += 1 + Time.deltaTime;
            }
            if (transform.position.y >= 90f)
            {
                if (doFade)
                {
                    StartCoroutine(FadeIn());
                    doFade = false;
                }

                //
            }
            if (transform.position.y >= 480.6f)
            {
                multiplier = 1;
                
            }

            if (transform.position.y >= 490f)
            {
                rb.velocity = Vector2.zero;
                multiplier = 0;
                isSpeedUpOn = false;
                Invoke("PopUpDialogue", 2.5f);
            }
        }
    }

    IEnumerator FadeIn()
    {

        float currenttime = 0f;
        while (currenttime < 2f)
        {
            currenttime += Time.deltaTime;
            cinemachineStoryboard.m_Alpha = currenttime/2;
            yield return null;
        }
        if (cinemachineStoryboard.m_Alpha > 1f)
        {
            cinemachineStoryboard.m_Alpha = 1;
            StartCoroutine(FadeOut());
        }
    }
    IEnumerator FadeOut()
    {

        float currenttime = 0.7f;
        while (currenttime > 0f)
        {
            currenttime -= Time.deltaTime;

            cinemachineStoryboard.m_Alpha = currenttime/0.7f;
            yield return null;
        }
        if (cinemachineStoryboard.m_Alpha < 0f)
            cinemachineStoryboard.m_Alpha = 0;
    }

   

    void PopUpDialogue()
    {
        Time.timeScale = 0f;
        dialogue1.SetActive(true);
        StartCoroutine(dialougeManager.Type());
    }
}
