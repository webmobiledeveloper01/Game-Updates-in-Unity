using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ReviveManager : MonoBehaviour
{
    public Text timeText;
    public Button reviveBTN;
    public Button reviveBTNcoin;
    private float time = 25f;

    public static ReviveManager instance;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    [HideInInspector] public bool _flag;


    private void OnEnable()
    {

        time = 25f;

        // if (_flag)

        //     img.gameObject.SetActive(false);
        // {
        //     timeText.text = "Dont loose hope try again!";

        //     reviveBTN.gameObject.SetActive(false);
        //     reviveBTNcoin.gameObject.SetActive(false);
        // }


    }
    public Image img;
    void Update()
    {
        // if (!_flag)
        // {
            time -= Time.deltaTime;
            img.fillAmount = time / 25f;

            timeText.text = Mathf.FloorToInt(time).ToString();
            if (time <= 0)
            {
                img.fillAmount = 0;
                timeText.text = "0";
                reviveBTN.interactable = false;
                reviveBTNcoin.interactable = false;

                GameManager.Instance.GameOver();
                gameObject.SetActive(false);

            }

        // }

        if (PlayerPrefs.GetInt("SGLIB_COINS") < 150)
        {
            reviveBTNcoin.interactable = false;
        }
    }

    public void RevivePressed()
    {
        // _flag = true;
        adSahil.instance.ShowRewardedAd();
    }

    public void RevivePressedCOIN()
    {
        // _flag = true;
        int coin = PlayerPrefs.GetInt("SGLIB_COINS", 0);

        coin -= 150;

        PlayerPrefs.SetInt("SGLIB_COINS", coin);

        PlayerController.instance._time = 5f;
        ReviveManager.instance.gameObject.SetActive(false);
        GameManager.Instance.StartGame();


    }


    public void DIE()
    {
        _flag = true;
        GameManager.Instance.GameOver();
    }
}
