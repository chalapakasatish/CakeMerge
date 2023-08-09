using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;
    [SerializeField]private int coins;
    public TMP_Text currencytext;
    public int Coins { get => coins; set => coins = value; }
    private void Awake()
    {
        Instance = this;
    }
    public void Start()
    {
        coins = PlayerPrefs.GetInt("Coins",coins);
        currencytext.text = coins.ToString();
    }
    public void AddCurrency(int num)
    {
        coins += num;
        currencytext.text = coins.ToString();
        PlayerPrefs.SetInt("Coins", coins);
    }
}
