using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class RewardedAdsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    //[SerializeField] Button _showAdButton;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    public string _adUnitId; // This will remain null for unsupported platforms
    //[SerializeField] CurrencyManager currencyManager;
    void Awake()
    {
        /*       // Get the Ad Unit ID for the current platform:
       #if UNITY_IOS
               _adUnitId = _iOSAdUnitId;
       #elif UNITY_ANDROID
               _adUnitId = _androidAdUnitId;
       #endif*/
        _adUnitId = _androidAdUnitId;/*(Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSAdUnitId
            : _androidAdUnitId;*/
        //Disable the button until the ad is ready to show:
        //_showAdButton = GameManager.instance.watchAdButton;
        //GameManager.instance.watchAdButton.interactable = false;
        LoadAd();
    }
 
    // Load content to the Ad Unit:
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }
 
    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);
 
        if (adUnitId.Equals(_adUnitId))
        {
            // Configure the button to call the ShowAd() method when clicked:
            //GameManager.instance.watchAdButton.onClick.AddListener(ShowAd);
            // Enable the button for users to click:
            //GameManager.instance.watchAdButton.interactable = true;
        }
    }
    public string rewardName;
    // Implement a method to execute when the user clicks the button:
    /*
    public void ShowAd()
    {
        // Disable the button:
        //_showAdButton.interactable = false;
        // Then show the ad:
        rewardName = "FreeCoins";
        Advertisement.Show(_adUnitId, this);
        Debug.Log("Unity Ads Rewarded Ad Completed");
        // Grant a reward.
        switch (rewardName)
        {
            case "FreeCoins":
                GameManager.instance.currencyManager.AddCurrency(100);
                //ScoreManager.instance.WatchAddCoinBalance(100);
                Debug.Log("You have gained 100 coins");
                break;
            case "_2XCoins":
                //ScoreManager.instance.Add2XCoinBalance(2);
                Debug.Log("You have gained 2X coins");
                break;
        }
        // Load another ad:
        Advertisement.Load(_adUnitId, this);
    }
    */
    
    public void ShowAd(string rewardType)
    {
        rewardName = rewardType;
        Advertisement.Show(_adUnitId, this);
        Debug.Log("Unity Ads Rewarded Ad Completed");
        // Grant a reward.
        switch (rewardName)
        {
            case "FreeCoins":
                //ScoreManager.instance.WatchAddCoinBalance(100);
                GameManager.instance.currencyManager.AddCurrency(100);
                Debug.Log("You have gained 100 coins");
                break;
            case "AddHowManyChances":
                //ScoreManager.instance.Add2XCoinBalance(2);
                CurrencyManager.Instance.AddHowManyChances(5);
                Debug.Log("You have gained 2X coins");
                break;
        }
        // Load another ad:
        Advertisement.Load(_adUnitId, this);
    }
    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        //if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        //{
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward.
            switch (rewardName)
            {
                case "FreeCoins":
                    //ScoreManager.instance.WatchAddCoinBalance(100);
                    Debug.Log("You have gained 100 coins");
                    break;
                case "_2XCoins":
                    //ScoreManager.instance.Add2XCoinBalance(2);
                    Debug.Log("You have gained 2X coins");
                    break;
            }
            // Load another ad:
            Advertisement.Load(_adUnitId, this);
        //}
    }
 
    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        LoadAd();
        // Use the error details to determine whether to try to load another ad.
    }
 
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        LoadAd();
        // Use the error details to determine whether to try to load another ad.
    }
 
    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
 
    void OnDestroy()
    {
        // Clean up the button listeners:
        //GameManager.instance.watchAdButton.onClick.RemoveAllListeners();
    }
}