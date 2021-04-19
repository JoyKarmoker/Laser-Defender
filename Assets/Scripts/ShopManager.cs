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
    [SerializeField] int crystalAvailable;
    [SerializeField] GameObject[] ShipPurchaseButtons;
    [SerializeField] GameObject[] ShipCardButtons;
    [SerializeField] GameObject[] PropsButtons;
    [SerializeField] TextMeshProUGUI Coins;
    [SerializeField] TextMeshProUGUI Crystals;
    [Tooltip("For stoping user to close shop menu during animation")]
    [SerializeField] Button[] ConfidentialButon;

    private void Start()
    {

        ES3.Save<int>(AllStringConstants.COINS_AVAILABILITY_STATUS, coinAvailable);
        ES3.Save<int>(AllStringConstants.CRYSTALS_AVAILABILITY_STATUS, coinAvailable);
        ShipPurchaseButtonsActionsOnStart();
        ShipCardButtonsActionOnStart();
        PropsButonsActionOnStart();
        GoodsStatActionOnStart();


    }



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
            ES3.DeleteFile();
    }


    #region Start section start..
    private void ShipCardButtonsActionOnStart()
    {
        for (int i = 0; i < ShipCardButtons.Length; i++)
        {
            if (ES3.Load<bool>(ShipCardButtons[i].tag, false))
            {
                ShipCardButtons[i].transform.parent.GetChild(1).gameObject.SetActive(true);
                ShipCardButtons[i].transform.GetChild(2).gameObject.SetActive(false);
                //remove uishine from object
                Destroy(ShipCardButtons[i].GetComponent<UIShiny>());
                //set alpha to 100 of parent
                Image img = ShipCardButtons[i].GetComponent<Image>();
                img.color = new Color(img.color.r,img.color.g, img.color.b, 100f);


                int availableCardIndex = ShipCardButtons[i].transform.parent.childCount;
                int currentCardIndex = ShipCardButtons[i].transform.GetSiblingIndex();
                if (currentCardIndex + 1 < availableCardIndex)
                {
                    ShipCardButtons[i].transform.parent.GetChild(currentCardIndex + 1).GetChild(1).GetComponent<UIEffect>().enabled = false;
                    ShipCardButtons[i].transform.parent.GetChild(currentCardIndex + 1).GetChild(3).gameObject.SetActive(false);
                }
            }
        }
    }

    private void ShipPurchaseButtonsActionsOnStart()
    {
        for (int i = 0; i < ShipPurchaseButtons.Length; i++)
        {
            if (ES3.Load<bool>(ShipPurchaseButtons[i].tag, false))
            {
                ShipPurchaseButtons[i].transform.parent.GetChild(1).gameObject.SetActive(true);
                ShipPurchaseButtons[i].gameObject.SetActive(false);
            }
        }
    }
    private void PropsButonsActionOnStart()
    {
        for (int i = 0; i < PropsButtons.Length; i++)
        {
            int curentLevel = ES3.Load<int>(PropsButtons[i].tag, 0);
            //if max out
            if (curentLevel >= 3)
            {
                PropsButtons[i].transform.GetChild(0).GetChild(0).GetComponent<Button>().interactable = false; // this button
                PropsButtons[i].transform.GetChild(0).GetChild(0).GetComponent<UIShiny>().enabled = false; // this btn
                PropsButtons[i].transform.GetChild(0).GetChild(1).gameObject.SetActive(false); // arrow 
                PropsButtons[i].transform.GetChild(1).GetChild(1).GetComponent<UIShiny>().enabled = false;
                PropsButtons[i].transform.GetChild(2).gameObject.SetActive(true); // max out panel
                PropsButtons[i].transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<Animation>().enabled = false;
            }

            PropsButtons[i].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = GameDataManager.GetPropsCostData(curentLevel, PropsButtons[i].tag).ToString();
            PropsButtons[i].transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = "+ " +
                    (GameDataManager.GetPropsPowerData(curentLevel + 1, PropsButtons[i].tag) - GameDataManager.GetPropsPowerData(curentLevel, PropsButtons[i].tag)).ToString();
            

            int thisPropIndexInPropsPanel = PropsButtons[i].transform.GetSiblingIndex();
            PropsButtons[i].transform.parent.parent.GetChild(thisPropIndexInPropsPanel + 1).GetChild(2).GetComponent<TextMeshProUGUI>().text =
                GameDataManager.GetPropsPowerData(curentLevel, PropsButtons[i].tag).ToString();
            PropsButtons[i].transform.parent.parent.GetChild(thisPropIndexInPropsPanel + 1).GetChild(3).GetComponent<Slider>().value =
                    curentLevel + 1;



        }
    }
    private void GoodsStatActionOnStart()
    {
        Coins.text = ES3.Load<int>("Coins", 0).ToString();
        Crystals.text = ES3.Load<int>("Crystals", 0).ToString();
        //coinAvailable = 0;
    }


    #endregion

    //called when ship purchase button pressed.
    public void OnShipPurchaseButtonClick()
    {
        // ship purchase button clicked
        GameObject clickedButton =  EventSystem.current.currentSelectedGameObject;
        //check if its coin or crystal and do transaction
        TextMeshProUGUI txt = clickedButton.GetComponentInChildren<TextMeshProUGUI>();
        int val = UpdateCoinsAndCrystalsStatus(txt);
        
        if(val == 1)
        {
            //update Goods
            UpdateGoodsValueOnUI();

            //save info
            ES3.Save<bool>(clickedButton.gameObject.tag, true);
            ES3.Save<bool>(clickedButton.transform.parent.parent.GetChild(4).GetChild(5).GetChild(0).tag, true); // card 1 of tha ship..
            // show buy message, disable this button and show upgrade button
            clickedButton.transform.parent.GetChild(1).gameObject.SetActive(true); // next button
            clickedButton.SetActive(false); // this button
            clickedButton.transform.parent.parent.GetChild(5).gameObject.SetActive(true); // enable congrats panel
                                                                                          //play Audio
            PlayShipPurchaseAudio();
        }

         //else move to shop page 4
         else
         {
             PlayErrorAudio();
             clickedButton.transform.parent.parent.gameObject.SetActive(false); // disable current page
             clickedButton.transform.parent.parent.parent.GetChild(6).gameObject.SetActive(true); // enable shop page
         }
         



    }

    //called when ship lvl unlock button clicked
    public void OnShipLevelUnlockButtonClick()
    {
        // ship lvl unlock button clicked
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;

        //check if its coin or crystal and do transaction
        TextMeshProUGUI txt = clickedButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        int val = UpdateCoinsAndCrystalsStatus(txt);

        if (val == 1)
        {
            //update Goods
            UpdateGoodsValueOnUI();
            
            //update info
            ES3.Save<bool>(clickedButton.transform.parent.gameObject.tag, true);

            //if coin available  disable this button
            clickedButton.SetActive(false);

            //remove uishine from parent
            Destroy(clickedButton.transform.parent.GetComponent<UIShiny>());

            //set alpha to 100 of parent
            clickedButton.transform.parent.GetComponent<Image>().color = new Color(clickedButton.transform.parent.GetComponent<Image>().color.r,
                clickedButton.transform.parent.GetComponent<Image>().color.g, clickedButton.transform.parent.GetComponent<Image>().color.b, 100f);


            //if next card available remove all effects from them..
            int availableCardIndex = clickedButton.transform.parent.parent.childCount;

            int currentCardIndex = clickedButton.transform.parent.GetSiblingIndex();

            if (currentCardIndex + 1 < availableCardIndex)
            {
                clickedButton.transform.parent.parent.GetChild(currentCardIndex + 1).GetChild(1).GetComponent<UIEffect>().enabled = false;
                clickedButton.transform.parent.parent.GetChild(currentCardIndex + 1).GetChild(3).GetComponent<UIDissolve>().Play();
                StartCoroutine(TurnOffUIDissolve(clickedButton.transform.parent.parent.
                    GetChild(currentCardIndex + 1).GetChild(3).gameObject, clickedButton.transform.parent.parent.
                    GetChild(currentCardIndex + 1).GetChild(3).GetComponent<UIDissolve>().effectPlayer.duration + 0.2f));
            }

            //play Audio
            PlayShipUnlockPurchaseAudio();

        }

        //else move to shop page 4
        else
        {
            PlayErrorAudio();
            clickedButton.transform.parent.parent.parent.gameObject.SetActive(false); // disable current page
            clickedButton.transform.parent.parent.parent.parent.gameObject.SetActive(false); // disable main ship page
            clickedButton.transform.parent.parent.parent.parent.parent.GetChild(6).gameObject.SetActive(true); // enable shop page
        }
    }

    IEnumerator TurnOffUIDissolve(GameObject lockPanel, float time)
    {
        yield return new WaitForSeconds(time);
        lockPanel.SetActive(false);
    }



    //called when props upgrade panel buttons clicked
    public void OnPropsUpgradeButtonClick()
    {
        //props upgrade  button clicked
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;

        //check if its coin or crystal and do transaction
        TextMeshProUGUI txt = clickedButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        int val = UpdateCoinsAndCrystalsStatus(txt);

        if (val == 1)
        {
            //update Goods
            UpdateGoodsValueOnUI();

            //save the next level info
            GameObject propsInfo = clickedButton.transform.parent.parent.gameObject;
            //get prev level
            int previousPropsLevel = ES3.Load<int>(propsInfo.tag, 0);
            //save current level
            ES3.Save<int>(propsInfo.tag, previousPropsLevel + 1);
            //update props cost data
            clickedButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                GameDataManager.GetPropsCostData(previousPropsLevel + 1, propsInfo.tag).ToString();

            //update props power data
            clickedButton.transform.parent.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = "+ " +
               (GameDataManager.GetPropsPowerData(previousPropsLevel + 2, propsInfo.tag) - GameDataManager.GetPropsPowerData(previousPropsLevel + 1, propsInfo.tag)).ToString();

            //scroll slider and play sound
            StartCoroutine(BarProgress(clickedButton.transform.parent.parent.GetChild(1).GetComponent<Slider>(), previousPropsLevel + 1)); // bar

            //update all info for main upgrade panel
                //show props power
                //show bar value
            int thisPropIndexInPropsPanel = clickedButton.transform.parent.parent.GetSiblingIndex();
            clickedButton.transform.parent.parent.parent.parent.GetChild(thisPropIndexInPropsPanel + 1).GetChild(2).GetComponent<TextMeshProUGUI>().text =
                GameDataManager.GetPropsPowerData(previousPropsLevel + 1, propsInfo.tag).ToString();
            clickedButton.transform.parent.parent.parent.parent.GetChild(thisPropIndexInPropsPanel + 1).GetChild(3).GetComponent<Slider>().value =
                previousPropsLevel + 2;
            
            // after scrolling if max out open max out panel( this is done inside the coroutine) and
            //deactive the button
            if (previousPropsLevel + 1 >= 3)
            {
                clickedButton.GetComponent<Button>().interactable = false; // this button
                clickedButton.GetComponent<UIShiny>().enabled = false; // this btn
                clickedButton.transform.parent.GetChild(1).gameObject.SetActive(false); // arrow 
                clickedButton.transform.parent.parent.GetChild(1).GetChild(1).GetComponent<UIShiny>().enabled = false;
                clickedButton.transform.parent.GetChild(2).GetChild(1).GetComponent<Animation>().enabled = false;
            }

            // else: update next updateable text


            //play Audio
            //play purchased audio..
            PlayGoodsPurchasedAudio();
            PlayPropsUpgradeAudio();

        }

        //else move to shop page 4
        else
        {
            //play error audio
            PlayErrorAudio();
            clickedButton.transform.parent.parent.parent.gameObject.SetActive(false); // disable current page
            clickedButton.transform.parent.parent.parent.parent.GetChild(6).gameObject.SetActive(true); // enable disabled back button 
            clickedButton.transform.parent.parent.parent.parent.gameObject.SetActive(false); // disable upgrade page
            clickedButton.transform.parent.parent.parent.parent.parent.gameObject.SetActive(false); // disable ship main page
            clickedButton.transform.parent.parent.parent.parent.parent.parent.GetChild(6).gameObject.SetActive(true); // enable shop page
        }
    }
    IEnumerator BarProgress(Slider slider, int level)
    {
        float currenttime = 0f;
        while (currenttime < 1f)
        {
            currenttime += Time.deltaTime;
            slider.value = currenttime + level;
            yield return null;
        }
        if (level >= 3)
            slider.transform.parent.GetChild(2).gameObject.SetActive(true); // max out panel

    }

    //called when goods purchase buttons clicked
    public void OnGoodsPurchaseButtonClick()
    {
        // coin purchase button clicked
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;

        if (clickedButton.tag == AllStringConstants.GOODS1_COINS)
        {
            //check the price and available coin
            int price = 60;
            crystalAvailable = ES3.Load<int>(AllStringConstants.CRYSTALS_AVAILABILITY_STATUS, 0);
            if (crystalAvailable >= price)
            {
                crystalAvailable -= price;

                ES3.Save<int>(AllStringConstants.CRYSTALS_AVAILABILITY_STATUS, crystalAvailable);
                //update Goods
                UpdateGoodsValueOnUI();

                int avaiableCoin = ES3.Load<int>(AllStringConstants.COINS_AVAILABILITY_STATUS, 0);
                //update coins
                ES3.Save<int>(AllStringConstants.COINS_AVAILABILITY_STATUS, avaiableCoin + 1000);

                //play anim..
                StartCoroutine(PlayAnimation(clickedButton.transform.parent.GetComponent<Animation>()));
                StartCoroutine(GoodsUpdate(Coins, 1000, avaiableCoin));

                //play purchased audio..
                PlayGoodsPurchasedAudio();
            }
            else
            {
                StartCoroutine(PlayAnimation(Crystals.transform.parent.gameObject.GetComponent<Animation>()));// coin not available animation...
                PlayErrorAudio();
            }
        }
        else if (clickedButton.tag == AllStringConstants.GOODS2_COINS)
        {
            //check the price and available coin
            int price = 500;
            crystalAvailable = ES3.Load<int>(AllStringConstants.CRYSTALS_AVAILABILITY_STATUS, 0);
            if (crystalAvailable >= price)
            {
                crystalAvailable -= price;

                ES3.Save<int>(AllStringConstants.CRYSTALS_AVAILABILITY_STATUS, crystalAvailable);
                //update Goods
                UpdateGoodsValueOnUI();

                int avaiableCoin = ES3.Load<int>(AllStringConstants.COINS_AVAILABILITY_STATUS, 0);
                //update coins
                ES3.Save<int>(AllStringConstants.COINS_AVAILABILITY_STATUS, avaiableCoin + 10000);

                //play anim..
                StartCoroutine(PlayAnimation(clickedButton.transform.parent.GetComponent<Animation>()));
                StartCoroutine(GoodsUpdate(Coins, 10000, avaiableCoin));

                //play purchased audio..
                PlayGoodsPurchasedAudio();
            }
            else
            {
                StartCoroutine(PlayAnimation(Crystals.transform.parent.gameObject.GetComponent<Animation>()));// coin not available animation...
                PlayErrorAudio();
            }
        }
        else if (clickedButton.tag == AllStringConstants.GOODS3_CRYSTALS)
        {
            //TODO:: // perfom in app purchase api works
        }
        else if (clickedButton.tag == AllStringConstants.GOODS4_CRYSTALS)
        {
            //TODO:: // perfom in app purchase api works
        }

        
        //TODO:: // play audio
    }
    IEnumerator GoodsUpdate(TextMeshProUGUI my_text, int amount, int currentAmount)
    {
        int displayScore = amount - 100;
        while (true)
        {
            yield return new WaitForSeconds(0);

            if (displayScore < amount)
            {
                displayScore++; //Increment the display score by 1
                my_text.text = (currentAmount + displayScore).ToString(); //Write it to the UI
            }
            else
                break;
        }
    }
    IEnumerator PlayAnimation(Animation animation)
    {
        animation.Play();

        for (int i = 0; i < ConfidentialButon.Length; i++)
        {
            ConfidentialButon[i].interactable = false;
        }
        
        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < ConfidentialButon.Length; i++)
        {
            ConfidentialButon[i].interactable = true;
        }


    }

    //updates coins and crystal values on UI
    void UpdateGoodsValueOnUI()
    {
        Coins.text = ES3.Load<int>(AllStringConstants.COINS_AVAILABILITY_STATUS, 0).ToString();
        Crystals.text = ES3.Load<int>(AllStringConstants.CRYSTALS_AVAILABILITY_STATUS, 0).ToString();
    }


    /// <summary>
    /// check for price. if affordable then do transaction..
    /// </summary>
    /// <param name="txt"></param>
    /// <returns></returns>
    private int UpdateCoinsAndCrystalsStatus(TextMeshProUGUI txt)
    {
        // check the price
        int price = Convert.ToInt32(txt.text);

        if (txt.tag == AllStringConstants.IT_IS_COIN)
        {
            coinAvailable = ES3.Load<int>(AllStringConstants.COINS_AVAILABILITY_STATUS, 0);

            if (coinAvailable >= price)
            {
                coinAvailable -= price;
                ES3.Save<int>(AllStringConstants.COINS_AVAILABILITY_STATUS, coinAvailable);
            }
            else
                return 0;

        }
        else if (txt.tag == AllStringConstants.IT_IS_CRYSTAL)
        {
            crystalAvailable = ES3.Load<int>(AllStringConstants.CRYSTALS_AVAILABILITY_STATUS, 0);

            if (crystalAvailable >= price)
            {
                crystalAvailable -= price;
                ES3.Save<int>(AllStringConstants.CRYSTALS_AVAILABILITY_STATUS, crystalAvailable);
            }
            else
                return 0;
        }
        return 1;
    }
    #region AudioMethods

    //plays the audio when ship is purchased
    public void PlayShipPurchaseAudio()
    {
        AudioManager.instance.play(AllStringConstants.CONGRATULATIONS_SOUND, false, true);
    }

    //plays the audio when shiplevel is purchased
    public void PlayShipUnlockPurchaseAudio()
    {
        AudioManager.instance.play(AllStringConstants.SHIPLEVELUNLOCK_SOUND, false, true);
    }

    //plays the audio when props is purchased
    public void PlayPropsUpgradeAudio()
    {
        AudioManager.instance.play(AllStringConstants.PROGRESS_SOUND, false, true);
    }

    //plays the audio when goods are purchased
    public void PlayGoodsPurchasedAudio()
    {
        AudioManager.instance.play(AllStringConstants.PURCHASE_SOUND, false, true);
    }

    //plays when not enough money or has an error
    public void PlayErrorAudio()
    {
        AudioManager.instance.play(AllStringConstants.ERROR_SOUND, false, true);
    }

    #endregion
}
