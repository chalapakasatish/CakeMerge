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
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        GameManager.instance.watchAdButton.onClick.AddListener(() => AdsInitializer.instance.rewardedAdsButton.ShowAd());
        GameManager.instance.levelCompleteWatchAdButton.onClick.AddListener(() => AdsInitializer.instance.rewardedAdsButton.ShowAd("_2XCoins"));
    }
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        uiManager.GameStateChangedCallback(GameState.MENU);
        //SetGameState(GameState.MENU);
    }
    public void StartGame()
    {
        uiManager.GameStateChangedCallback(GameState.GAME);
        //SetGameState(GameState.GAME);
    }
    public void StartShop()
    {
        uiManager.GameStateChangedCallback(GameState.SHOP);
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
