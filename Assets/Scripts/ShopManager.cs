using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;
using Coffee.UIEffects;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    [SerializeField] int coinAvailable;

    private void Start()
    {
        //if ship purchased disable the button.
    }

    //called when ship purchase button pressed.
    public void OnShipPurchaseButtonClick()
    {
        // ship purchase button clicked
        GameObject clickedButton =  EventSystem.current.currentSelectedGameObject;
        //check the price and available coin
        int price = Convert.ToInt32(clickedButton.GetComponentInChildren<TextMeshProUGUI>().text);
        if (coinAvailable >= price)
        {
            coinAvailable -= price;
            //if coin available show buy message disable this button and show upgrade button
            clickedButton.transform.parent.GetChild(1).gameObject.SetActive(true);
            clickedButton.SetActive(false);
            clickedButton.transform.parent.parent.GetChild(7).gameObject.SetActive(true); // enable congrats panel
            //play Audio
            PlayShipPurchaseAudio();
        }
        
        //else move to shop page 4
        else
        {
            PlayErrorAudio();
            clickedButton.transform.parent.parent.gameObject.SetActive(false); // disable current page
            clickedButton.transform.parent.parent.parent.GetChild(4).gameObject.SetActive(true); // enable shop page
        }


        

    }

    //called when ship lvl unlock button clicked
    public void OnShipLevelUnlockButtonClick()
    {
        // ship lvl unlock button clicked
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        //check the price and available coin
        int price = Convert.ToInt32(clickedButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text);

        if (coinAvailable >= price)
        {
            coinAvailable -= price;
            //if coin available  disable this button
            clickedButton.SetActive(false);
            //remove uishine from parent
            Destroy(clickedButton.transform.parent.GetComponent<UIShiny>());
            //set alpha to 100 of parent
            clickedButton.transform.parent.GetComponent<Image>().color = new Color(clickedButton.transform.parent.GetComponent<Image>().color.r,
                clickedButton.transform.parent.GetComponent<Image>().color.g, clickedButton.transform.parent.GetComponent<Image>().color.b, 100f);
            //play Audio
            PlayShipPurchaseAudio();

        }

        //else move to shop page 4
        else
        {
            PlayErrorAudio();
            clickedButton.transform.parent.parent.parent.gameObject.SetActive(false); // disable current page
            clickedButton.transform.parent.parent.parent.parent.parent.GetChild(4).gameObject.SetActive(true); // enable shop page
        }
    }
    //called when props upgrade panel buttons clicked
    public void OnPropsUpgradeButtonClick()
    {
        //props upgrade  button clicked
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
        //check the price and available coin as well as if we can upgrade more or not?
        int price = Convert.ToInt32(clickedButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text);

        if (coinAvailable >= price)
        {
            //TODO:
            //minus the coin
            //update props power data
            //scroll slider and play sound
            // after scrolling if max out open max out panel 
              // else: update next updateable text

        }

        //else move to shop page 4
        else
        {
            //play error audio
            PlayErrorAudio();
            clickedButton.transform.parent.parent.parent.gameObject.SetActive(false); // disable current page
            clickedButton.transform.parent.parent.parent.parent.gameObject.SetActive(false); // disable upgrade page
            clickedButton.transform.parent.parent.parent.parent.parent.gameObject.SetActive(false); // disable ship main page
            clickedButton.transform.parent.parent.parent.parent.parent.parent.GetChild(4).gameObject.SetActive(true); // enable shop page
        }
    }

    //called when goods purchase buttons clicked
    public void OnGoodsPurchaseButtonClick()
    {
        //TODO:
        //if have enough gems to buy coin
            //purchase coin, do animation, play sfx, update balance
        //else: show a message (not enough value...)

        //if real time money purchase called
            // perfom in app purchase api works
            //update balance, play animation, play audio
    }
    #region AudioMethods
    //plays the audio when ship is purchased
    public void PlayShipPurchaseAudio()
    {
        audio_Manager.instance.play("congratulations", false);
    }

    //plays when not enough money or has an error
    public void PlayErrorAudio()
    {
        audio_Manager.instance.play("error", false);
    }

    #endregion
}
