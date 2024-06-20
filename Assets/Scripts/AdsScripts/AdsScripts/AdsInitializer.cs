using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    public static AdsInitializer instance;
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    public string _gameId;
    public RewardedAdsButton rewardedAdsButton;
    public InterstitialAdExample interstitialAdExample;
    public BannerAdExample bannerAdExample;
    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
      /*  else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
      */
        
        InitializeAds();
    }

    public void InitializeAds()
    {
        _gameId = _androidGameId;/*(Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;*/
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        
        rewardedAdsButton.LoadAd();
        interstitialAdExample.LoadAd();
        bannerAdExample.LoadBanner();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}