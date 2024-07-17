using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, IGameStateListner
{
    [SerializeField] private GameObject menuPanel, gamePanel,settingsPanel, shopPanel, pausePanel, watchAdPopUpPanel, gameoverPanel;
    List<GameObject> panels = new List<GameObject>();
    public GameState gameState;
    private void Awake()
    {
        panels.AddRange(new GameObject[] { menuPanel, gamePanel, settingsPanel, shopPanel , pausePanel, watchAdPopUpPanel, gameoverPanel });
    }
    public void GameStateChangedCallback(GameState _gameState)
    {
        switch (_gameState)
        {
            case GameState.MENU:
                ShowPanel(menuPanel);
                break;
            case GameState.GAME:
                ShowPanel(gamePanel);
                break;
            case GameState.PAUSE:
                ShowPanel(pausePanel);
                break;
            case GameState.SETTINGS:
                ShowPanel(settingsPanel);
                break;
            case GameState.SHOP:
                ShowPanel(shopPanel);
                break;
            case GameState.WATCHADPOPUPPANEL:
                ShowPanel(watchAdPopUpPanel);
                break;
            case GameState.GAMEOVER:
                ShowPanel(gameoverPanel);
                break;
        }
        gameState = _gameState;
        Debug.Log(gameState.ToString());
    }
    public void ShowPanel(GameObject panel)
    {
        foreach (GameObject go in panels)
        {
            go.SetActive(go == panel);

            //if(go == panel)
            //{
            //    panel.SetActive(true);
            //}
            //else
            //{
            //    panel.SetActive(false);
            //}
        }
    }
}
