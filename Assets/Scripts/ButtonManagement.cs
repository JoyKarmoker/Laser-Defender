using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Coffee.UIEffects;
using UnityEngine.SceneManagement;
using System;

public class ButtonManagement : MonoBehaviour
{

    [SerializeField] GameObject map, EndlessButtonHolder;
    public void ExitButton()
    {

        Application.Quit();
    }

    public void Mapclose()
    {
        map.GetComponentInChildren<ScrollRect>().verticalNormalizedPosition = 0;
    }

    // calls when endless mode button pressed..
    public void OnEndlessModeButtonPressed()
    {
        //TODO::....
    }

    //when play button pressed..
    public void OnPlayButtonPressed()
    {
        int storymodeStatus = ES3.Load<int>(AllStringConstants.STORY_MODE_STATUS, 0);
        //Check if Endless mode unlocked..
        //storymodeStatus == 0 means locked


        if (storymodeStatus == 1) // unlocked..
        {

            StartCoroutine(DarkenThePanel(1.5f));
            EndlessButtonHolder.transform.GetChild(1).GetComponent<UIDissolve>().Play(); // play dissolve
            EndlessButtonHolder.transform.GetChild(2).gameObject.SetActive(true); // activate particles effect
            ES3.Save<int>(AllStringConstants.STORY_MODE_STATUS, 2);
        }
        else if (storymodeStatus == 2) // unlocked and seen animations..
        {
            EndlessButtonHolder.transform.GetChild(1).gameObject.SetActive(false); // disable cover.
            EndlessButtonHolder.transform.GetChild(0).GetComponent<UIEffect>().enabled = false; // deactive uieffect.

        }
    }

    /// <summary>
    /// calles when endless mode unlocked..
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    IEnumerator DarkenThePanel(float seconds)
    {
        UIEffect grayEffect =  EndlessButtonHolder.transform.GetChild(0).GetComponent<UIEffect>();
        GameObject panel = EndlessButtonHolder.transform.parent.GetChild(2).gameObject;
        panel.SetActive(true);
        CanvasGroup img = panel.GetComponent<CanvasGroup>();

        EndlessButtonHolder.transform.GetChild(0).GetComponent<UIEffect>().effectFactor = 0; // disable grayscale

        float currentTime = 0;
        float imageCurrentAlpha = 0; // must go to 160.

        while (currentTime < seconds)
        {
            currentTime += Time.deltaTime;
            imageCurrentAlpha = (1*currentTime) / seconds;
            grayEffect.effectFactor = 1 - ((1 * currentTime) / seconds);
            img.alpha = imageCurrentAlpha;
            yield return null;
        }
        yield return new WaitForSeconds(4);
        grayEffect.enabled = false;

        StartCoroutine(BackToNormal(0.2f, img));
    }
    IEnumerator BackToNormal(float seconds, CanvasGroup img)
    {
        float currentTime = 0;
        float imageCurrentAlpha = 0; // must go to 160.

        while (currentTime < seconds)
        {
            currentTime += Time.deltaTime;
            imageCurrentAlpha = 1 - ((1 * currentTime) / seconds);
            img.alpha = imageCurrentAlpha;
            yield return null;
        }
        EndlessButtonHolder.transform.GetChild(1).gameObject.SetActive(false);
        img.gameObject.SetActive(false);
        EndlessButtonHolder.transform.GetChild(2).gameObject.SetActive(false); // deactivate particles effect

    }
}
