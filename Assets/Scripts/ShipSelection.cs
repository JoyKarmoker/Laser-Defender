using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Coffee.UIEffects;
using UnityEngine.UI;
public class ShipSelection : MonoBehaviour
{
    [Tooltip("Must follow the order. ascending order..")]
    [SerializeField] GameObject[] ships;
    public void OnSelectShip1()
    {
        GameSession.instance.selectedShip = 1;
    } 
    
    public void OnSelectShip2()
    {
        GameSession.instance.selectedShip = 2;
    } 
    
    public void OnSelectShip3()
    {
        GameSession.instance.selectedShip = 3;
    }

    public void UpdateShipStatus()
    {

        for (int i = 0; i < ships.Length; i++)
        {
            for(int j = 0; j < AllStringConstants.SHIPS_AVAILABILITY_STATUS.Length; j++) // checks which ship is available..
            {
                if(ES3.Load<bool>(AllStringConstants.SHIPS_AVAILABILITY_STATUS[j], false))
                {
                    ships[j].transform.GetChild(1).GetChild(0).GetComponent<UIEffect>().enabled = false;
                    ships[j].transform.GetChild(2).GetComponentInChildren<Button>().interactable = true;

                    for (int k = AllStringConstants.SHIPS_CARDS_AVAILABILITY[j].Length - 1; k >= 0; k--)
                    {
                        //Debug.LogError(AllStringConstants.SHIPS_CARDS_AVAILABILITY[j][k]);
                        if (ES3.Load<bool>(AllStringConstants.SHIPS_CARDS_AVAILABILITY[j][k], false)) {
                            ships[j].transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Level " + (k+1).ToString();
                            break;
                        }
                    }
                }
                else
                {
                    ships[j].transform.GetChild(1).GetChild(0).GetComponent<UIEffect>().enabled = true;
                    ships[j].transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Unlock it from shop";
                    ships[j].transform.GetChild(2).GetComponentInChildren<Button>().interactable = false;
                }
            }

        }
    }
}
