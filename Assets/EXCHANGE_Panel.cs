using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EXCHANGE_Panel : MonoBehaviour
{
    public GameObject successful_Object;
    public Button exchangeButton;


    private void Update()
    {
         if(PlayerPrefs.GetInt("SGLIB_COINS") < 100){
            exchangeButton.interactable = false;
         }
    }


    public void EXCHANGE_FUNCTION()
    {
        int coins = PlayerPrefs.GetInt("SGLIB_COINS");
        int panCake = PlayerPrefs.GetInt("CakeCount");

        coins -= 100;
        PlayerPrefs.SetInt("SGLIB_COINS", coins);
        panCake += 50;
        PlayerPrefs.SetInt("CakeCount", panCake);

        successful_Object.SetActive(true);
    }
}
