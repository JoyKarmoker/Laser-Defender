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
                    LevelLoader.instance.StartWhiteCrossfadeTransition();
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
                StartCoroutine(SetupPortal());
                //Invoke("PopUpDialogue", 2.5f);
            }
        }
    }


    IEnumerator SetupPortal()
    {

        yield return new WaitForSeconds(1f);
        portal.SetActive(true);
        cam.GetComponent<RipplePostProcessor>().SetPosition(new Vector3(98.5f, 91f, 0));

        yield return new WaitForSeconds(5f);
        LevelLoader.instance.StartSpikeTransition();
        yield return new WaitForSeconds(1f);
        cam.GetComponent<RipplePostProcessor>().enabled = false;

        yield return new WaitForSeconds(5f);
        portal.SetActive(false);
        LevelLoader.instance.StartSpikeTransition();
    }
    
    void PopUpDialogue()
    {
        Time.timeScale = 0f;
        dialogue1.SetActive(true);
        StartCoroutine(dialougeManager.Type());
    }
}
