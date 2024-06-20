using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class BannerAdExample : MonoBehaviour
{
    // For the purpose of this example, these buttons are for functionality testing:
    //[SerializeField] Button _loadBannerButton;
    //[SerializeField] Button _showBannerButton;
    //[SerializeField] Button _hideBannerButton;

    [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;

    [SerializeField] string _androidAdUnitId = "Banner_Android";
    [SerializeField] string _iOSAdUnitId = "Banner_iOS";
    public string _adUnitId; // This will remain null for unsupported platforms.

    void Start()
    {
        /*       // Get the Ad Unit ID for the current platform:
       #if UNITY_IOS
               _adUnitId = _iOSAdUnitId;
       #elif UNITY_ANDROID
               _adUnitId = _androidAdUnitId;
       #endif*/
        _adUnitId = _androidAdUnitId;
        // Disable the button until an ad is ready to show:
        //_showBannerButton.interactable = false;
        //_hideBannerButton.interactable = false;

        // Set the banner position:
        Advertisement.Banner.SetPosition(_bannerPosition);

        // Configure the Load Banner button to call the LoadBanner() method when clicked:
        //_loadBannerButton.onClick.AddListener(LoadBanner);
        //_loadBannerButton.interactable = true;
        //LoadBanner();
    }

    // Implement a method to call when the Load Banner button is clicked:
    public void LoadBanner()
    {
        // Set up options to notify the SDK of load events:
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        // Load the Ad Unit with banner content:
        Advertisement.Banner.Load(_adUnitId, options);
    }

    // Implement code to execute when the loadCallback event triggers:
    void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");
        //ShowBannerAd();
        //HideBannerAd();
        // Configure the Show Banner button to call the ShowBannerAd() method when clicked:
        //_showBannerButton.onClick.AddListener(ShowBannerAd);
        // Configure the Hide Banner button to call the HideBannerAd() method when clicked:
        //_hideBannerButton.onClick.AddListener(HideBannerAd);

        // Enable both buttons:
        //_showBannerButton.interactable = true;
        //_hideBannerButton.interactable = true;
    }

    // Implement code to execute when the load errorCallback event triggers:
    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
        // Optionally execute additional code, such as attempting to load another ad.
    }

    // Implement a method to call when the Show Banner button is clicked:
    public void ShowBannerAd()
    {
        //PlayerPrefs.SetInt("SHOW_ADS", 1);
        //if (PlayerPrefs.GetInt("SHOW_ADS") == 1)
        //{
        //    return;
        //}
        // Set up options to notify the SDK of show events:
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        // Show the loaded Banner Ad Unit:
        Advertisement.Banner.Show(_adUnitId, options);
        Debug.Log("Banner Show");
    }

    // Implement a method to call when the Hide Banner button is clicked:
    public void HideBannerAd()
    {
        // Hide the banner:
        Advertisement.Banner.Hide();
        Debug.Log("Banner Hide");
    }

    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }

    void OnDestroy()
    {
        // Clean up the listeners:
        //_loadBannerButton.onClick.RemoveAllListeners();
        //_showBannerButton.onClick.RemoveAllListeners();
        //_hideBannerButton.onClick.RemoveAllListeners();
    }
}
//Advertisement.Banner.SetPosition (BannerPosition.TOP_CENTER);