using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;
    [SerializeField]private int coins;
    public TMP_Text currencytext;
    public TMP_Text howManyClicksText,howManyChancesText;
    public TMP_Text PriceCakeText;
    [SerializeField] private int howManyClicks,howManyChances;
    [SerializeField] private int howManyClicksTextRep;
    public LevelManager levelManager;

    public int Coins { get => coins; set => coins = value; }
    public int HowManyClicks { get => howManyClicks; set => howManyClicks = value; }
    public int HowManyChances { get => howManyChances; set => howManyChances = value; }
    public int HowManyClicksTextRep { get => howManyClicksTextRep; set => howManyClicksTextRep = value; }

    private void Awake()
    {
        Instance = this;
    }
    public void Start()
    {
        coins = PlayerPrefs.GetInt("Coins",coins);
        howManyClicks = PlayerPrefs.GetInt("HowManyClicks",howManyClicks);
        howManyChances = PlayerPrefs.GetInt("HowManyChances", levelManager.howManyChances[levelManager.LevelCount].howmanyChances);
        currencytext.text = Coins.ToString();
        PriceCakeText.text = PlayerPrefs.GetInt("PriceCake",CakesManager.instance.Price).ToString();
        //howManyChancesText.text = howManyChances.ToString();
        RemoveHowManyChances(0);
    }
    public void AddCurrency(int num)
    {
        coins += num;
        currencytext.text = coins.ToString();
        PlayerPrefs.SetInt("Coins", coins);
    }
    public void RemoveCurrency(int num) 
    {
        coins -= num;
        if (coins <= 0)
        {
            coins = 0;
        }
        if (coins <= CakesManager.instance.Price)
        {
            CakesManager.instance.spawnCakeButton.GetComponent<Button>().interactable = false;
        }
        currencytext.text = coins.ToString();
        PlayerPrefs.SetInt("Coins", coins);
        PriceCakeText.text = CakesManager.instance.Price.ToString();  
    }
    public void AddHowManyClicks(int num) 
    {
        howManyClicks += num;
        PlayerPrefs.SetInt("HowManyClicks", howManyClicks);
        howManyClicksText.text = PlayerPrefs.GetInt("HowManyClicks").ToString() + "/" + PlayerPrefs.GetInt("TargetClicks").ToString();
        CakesManager.instance.CheckTargetClicks();
    }
    public void RemoveHowManyChances(int num)
    {
        howManyChances -= num;
        PlayerPrefs.SetInt("HowManyChances", howManyChances);
        howManyChancesText.text = PlayerPrefs.GetInt("HowManyChances").ToString();
        CakesManager.instance.CheckTargetClicks();
    }
    public void AddHowManyChances(int num)
    {
        howManyChances += num;
        PlayerPrefs.SetInt("HowManyChances", howManyChances);
        howManyChancesText.text = PlayerPrefs.GetInt("HowManyChances").ToString();
        CakesManager.instance.CheckTargetClicks();
    }
    public void GetHowManyChances()
    {
        howManyChances = levelManager.howManyChances[levelManager.LevelCount].howmanyChances;
        PlayerPrefs.SetInt("HowManyChances", howManyChances);
        howManyChancesText.text = PlayerPrefs.GetInt("HowManyChances").ToString();
    }
}
