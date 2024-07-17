using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UIManager uiManager;
    public CurrencyManager currencyManager;
    public Button watchAdButton, levelCompleteWatchAdButton;
    public GameObject particleCamera;
    public Canvas mainCanvas;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        //GameManager.instance.watchAdButton.onClick.AddListener(() => AdsInitializer.instance.rewardedAdsButton.ShowAd());
        //GameManager.instance.levelCompleteWatchAdButton.onClick.AddListener(() => AdsInitializer.instance.rewardedAdsButton.ShowAd("_2XCoins"));
    }
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        uiManager.GameStateChangedCallback(GameState.MENU);
        //SetGameState(GameState.MENU);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameManager.instance.uiManager.gameState == GameState.GAME)
            {
                uiManager.GameStateChangedCallback(GameState.PAUSE);
                PauseButton();
            }
        }
    }
    public void StartGame()
    {
        uiManager.GameStateChangedCallback(GameState.GAME);
        particleCamera.SetActive(false);
        mainCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        //SetGameState(GameState.GAME);
    }
    public void StartShop()
    {
        uiManager.GameStateChangedCallback(GameState.SHOP);
        //SetGameState(GameState.SHOP);
    }
    public void SettingsButton()
    {
        uiManager.GameStateChangedCallback(GameState.SETTINGS);
        //SetGameState(GameState.SHOP);
    }
    public void PauseButton()
    {
        Time.timeScale = 0;
    }
    public void ResumeButton()
    {
        Time.timeScale = 1;
        uiManager.GameStateChangedCallback(GameState.GAME);
    }
    public void HomeButton()
    {
        Time.timeScale = 1;
        uiManager.GameStateChangedCallback(GameState.MENU);
        mainCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        particleCamera.SetActive(true);
    }
    public void WatchAdPopupCloseButton()
    {
        uiManager.GameStateChangedCallback(GameState.GAMEOVER);
        Debug.Log("WatchAdPopupCloseButton");
        //SetGameState(GameState.SHOP);
    }
    public void SetGameState(GameState gameState)
    {
        //IEnumerable<IGameStateListner> gameStateListners =
        //   FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IGameStateListner>();

        //foreach (IGameStateListner gameStateListner in gameStateListners)
        //{
        //    gameStateListner.GameStateChangedCallback(gameState);
        //}
    }
    public void WaveCompletedCallback()
    {
        //if (Player.instance.HasLeveledUp())
        //{
        //    SetGameState(GameState.WAVETRANSITION);
        //}
        //else
        //{
        //    SetGameState(GameState.SHOP);
        //}
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("GameOpened", 0);
    }
    public void PrivacyPolicy()
    {
        Application.OpenURL("https://www.runelordegamesstudios.com/privacy-policy");
    }
    public void RateUs()
    {
        Application.OpenURL("https://www.runelordegamesstudios.com/privacy-policy");
    }
}
public interface IGameStateListner
{
    void GameStateChangedCallback(GameState gameState);
}
