using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class adSahil : MonoBehaviour
{

  public string _adUnitRewarded = "ca-app-pub-7868094223399674/6021241573";
  public string _adunitInterstitial = "ca-app-pub-7868094223399674/86474049142";

#if UNITY_ANDROID
  private string _adUnitId = "ca-app-pub-7868094223399674/2273568258";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
  private string _adUnitId = "unused";
#endif

  BannerView _bannerView;
  public static adSahil instance;
  private RewardedAd _rewardedAd;



  public void RemoveAds()
  {
    PlayerPrefs.SetInt("REMOVED_ADS", 1);
  }


  // Start is called before the first frame update
  void Start()
  {


    if (!PlayerPrefs.HasKey("REMOVED_ADS"))
    {



      instance = this;
      // Initialize the Google Mobile Ads SDK.
      MobileAds.Initialize((InitializationStatus initStatus) =>
      {
        // This callback is called once the MobileAds SDK is initialized.
      });

      InvokeRepeating("CreateBannerView", 0f, 5f);
      InvokeRepeating("LoadAd", 0f, 5f);
      InvokeRepeating("LoadInterstitialAd", 0f, 5f);
      InvokeRepeating("LoadRewardedAd", 0f, 5f);

      CreateBannerView();
      LoadAd();

    }
  }

  public void CreateBannerView()
  {
    Debug.Log("Creating banner view");

    // If we already have a banner, destroy the old one.
    if (_bannerView != null)
    {
      // DestroyAd();
    }

    // Create a 320x50 banner at top of the screen
    _bannerView = new BannerView(_adUnitId, AdSize.Banner, AdPosition.Bottom);
  }

  public void LoadAd()
  {
    // create an instance of a banner view first.
    if (_bannerView == null)
    {
      CreateBannerView();
    }

    // create our request used to load the ad.
    var adRequest = new AdRequest();

    // send the request to load the ad.
    Debug.Log("Loading banner ad.");
    _bannerView.LoadAd(adRequest);

  }

  public void LoadRewardedAd()
  {
    // Clean up the old ad before loading a new one.
    if (_rewardedAd != null)
    {
      _rewardedAd.Destroy();
      _rewardedAd = null;
    }

    Debug.Log("Loading the rewarded ad.");

    // create our request used to load the ad.
    var adRequest = new AdRequest();

    // send the request to load the ad.
    RewardedAd.Load(_adUnitRewarded, adRequest,
        (RewardedAd ad, LoadAdError error) =>
        {
          // if error is not null, the load request failed.
          if (error != null || ad == null)
          {
            Debug.LogError("Rewarded ad failed to load an ad " +
                             "with error : " + error);
            return;
          }

          Debug.Log("Rewarded ad loaded with response : "
                      + ad.GetResponseInfo());

          _rewardedAd = ad;
        });
  }

  public void DestroyBannerView()
  {
    if (_bannerView != null)
    {
      Debug.Log("Destroying banner view.");
      _bannerView.Destroy();
      _bannerView = null;
    }
  }

  public GameObject adsNotAvailabelPANEL;
  public void ShowRewardedAd()
  {
    const string rewardMsg =
        "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

    if (_rewardedAd != null && _rewardedAd.CanShowAd())
    {
      _rewardedAd.Show((Reward reward) =>
      {
        ReviveManager.instance._flag = true;
        PlayerController.instance.flag = false;
        PlayerController.instance._time = 5f;
        ReviveManager.instance.gameObject.SetActive(false);
        GameManager.Instance.StartGame();

        Debug.Log("USER REWARDED");
      });
    }
    else
    {
      adsNotAvailabelPANEL.SetActive(true);
    }
  }


  private InterstitialAd _interstitialAd;



  public void ShowInterstitialAd()
  {
    if (_interstitialAd != null && _interstitialAd.CanShowAd())
    {
      Debug.Log("Showing interstitial ad.");
      _interstitialAd.Show();
    }
    else
    {
      Debug.LogError("Interstitial ad is not ready yet.");
    }
  }
  /// <summary>
  /// Loads the interstitial ad.
  /// </summary>
  public void LoadInterstitialAd()
  {
    // Clean up the old ad before loading a new one.
    if (_interstitialAd != null)
    {
      _interstitialAd.Destroy();
      _interstitialAd = null;
    }

    Debug.Log("Loading the interstitial ad.");

    // create our request used to load the ad.
    var adRequest = new AdRequest();

    // send the request to load the ad.
    InterstitialAd.Load(_adunitInterstitial, adRequest,
        (InterstitialAd ad, LoadAdError error) =>
        {
          // if error is not null, the load request failed.
          if (error != null || ad == null)
          {
            Debug.LogError("interstitial ad failed to load an ad " +
                             "with error : " + error);
            return;
          }

          Debug.Log("Interstitial ad loaded with response : "
                      + ad.GetResponseInfo());

          _interstitialAd = ad;
        });
  }

}
