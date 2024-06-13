using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, IGameStateListner
{
    [SerializeField] private GameObject menuPanel, gamePanel, shopPanel;
    List<GameObject> panels = new List<GameObject>();
    private void Awake()
    {
        panels.AddRange(new GameObject[] { menuPanel, gamePanel, shopPanel });
    }
    public void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.MENU:
                ShowPanel(menuPanel);
                break;
            case GameState.GAME:
                ShowPanel(gamePanel);
                break;
            case GameState.SHOP:
                ShowPanel(shopPanel);
                break;
        }
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
