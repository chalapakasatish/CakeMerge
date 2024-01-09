using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class CakesManager : MonoBehaviour
{
    public static CakesManager instance;

    public List<GameObject> spawnPoints = new List<GameObject>();
    public List<bool> cakeExists = new List<bool>();
    public GameObject currentPoint;
    public List<GameObject> cakes = new List<GameObject>();
    public CameraController cameraController;
    public GameObject dollerEffect;
    public GameObject confettiEffect;
    public GameObject holderEffect;

    public GameObject destination;

    public GameObject emojis;
    public Belt belt1,belt2,belt3;
    public Button spawnCakeButton,continueButton,serveButton,autoMergeButton;
    public GameObject howManyCakesButton;
    public List<GameObject> cakesInstantiate = new List<GameObject>();
    public List<int> cakeNumberRemember = new List<int>();
    public GameObject points;
    [SerializeField]private int price;
    [SerializeField]private int cakePriceChange;
    [SerializeField] private int cakeNumber = 0;
    public int[] targetClicks;
    public bool serveStarted;
    public LevelManager levelManager;
    public int Price { get => price; set => price = value; }
    public int CakePriceChange { get => cakePriceChange; set => cakePriceChange = value; }
    public int CakeNumber { get => cakeNumber; set => cakeNumber = value; }
    public GameObject cakeReferenceImage;
    public Image[] cakeResourceImage;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        price = PlayerPrefs.GetInt("PriceCake", price);
        cakeNumber = PlayerPrefs.GetInt("CakeNumber", cakeNumber);
        CheckTargetClicks();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            //Application.Quit();
        }
    }
    public void CheckTargetClicks()
    {
        //howManyCakesButton.SetActive(true);
        //serveButton.gameObject.SetActive(true);
        if (PlayerPrefs.GetInt("HowManyClicks") >= 1)
        {
            serveButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            serveButton.GetComponent<Button>().interactable = false;
        }

        if (IsAvailableHolders() && CurrencyManager.Instance.Coins >= price)
        {
            spawnCakeButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            spawnCakeButton.GetComponent<Button>().interactable = false;
        }
        int number = PlayerPrefs.GetInt("HowManyClicks");
        // Testing Cakenumber
        //number = 40;
        CurrencyManager.Instance.howManyClicksText.text = number + "/" + PlayerPrefs.GetInt("TargetClicks").ToString();
        switch (number)
        {
            case 0:
                cakeNumber = 0;
                CakePriceChange = 1;
                CurrencyManager.Instance.HowManyClicksTextRep = targetClicks[0];
                PlayerPrefs.SetInt("TargetClicks", targetClicks[0]);
                PlayerPrefs.SetInt("CakeNumber", cakeNumber);
                break;
            case 40:
                cakeNumber = 1;
                CakePriceChange = 2;
                CurrencyManager.Instance.HowManyClicksTextRep = targetClicks[1];
                PlayerPrefs.SetInt("TargetClicks", CurrencyManager.Instance.HowManyClicksTextRep);
                PlayerPrefs.SetInt("CakeNumber", cakeNumber);
                break;
            case 80:
                cakeNumber = 2;
                CakePriceChange = 3;
                CurrencyManager.Instance.HowManyClicksTextRep = targetClicks[2];
                PlayerPrefs.SetInt("TargetClicks", CurrencyManager.Instance.HowManyClicksTextRep);
                PlayerPrefs.SetInt("CakeNumber", cakeNumber);
                break;
            case 120:
                cakeNumber = 3;
                CakePriceChange = 4;
                CurrencyManager.Instance.HowManyClicksTextRep = targetClicks[3];
                PlayerPrefs.SetInt("TargetClicks", CurrencyManager.Instance.HowManyClicksTextRep);
                PlayerPrefs.SetInt("CakeNumber", cakeNumber);
                break;
            case 160:
                cakeNumber = 4;
                CakePriceChange = 5;
                CurrencyManager.Instance.HowManyClicksTextRep = targetClicks[4];
                PlayerPrefs.SetInt("TargetClicks", CurrencyManager.Instance.HowManyClicksTextRep);
                PlayerPrefs.SetInt("CakeNumber", cakeNumber);
                break;
        }
        CurrencyManager.Instance.howManyClicksText.text = number + "/" + PlayerPrefs.GetInt("TargetClicks").ToString();

        for (int i = 0; i < cakeResourceImage.Length; i++)
        {
            if (i == PlayerPrefs.GetInt("CakeNumber"))
            {
                cakeReferenceImage.GetComponent<Image>().sprite = cakeResourceImage[i].sprite;
            }
        }
    }
    public void SpawnCake()
    {
        CheckTargetClicks();
        GetCake(cakeNumber);
    }
    public bool IsAvailableHolders()
    {
        for (int i = 0; i < cakeExists.Count; i++)
        {
            if (cakeExists[i] == false)
            {
                return true;
            }
        }
        return false;
    }

    public void GetCake(int num)
    {
        CurrencyManager.Instance.AddHowManyClicks(1);
        CurrencyManager.Instance.RemoveCurrency(price);
        Price += CakePriceChange;
        PlayerPrefs.SetInt("PriceCake", Price);
        CurrencyManager.Instance.PriceCakeText.text = PlayerPrefs.GetInt("PriceCake").ToString();
       

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if(spawnPoints[i] && !cakeExists[i])
            {
                currentPoint = spawnPoints[i];
                cakeExists[i] = true;
                GameObject mObject = Instantiate(cakes[num]);
                cakeNumberRemember[i] = num;
                PlayerPrefs.SetInt("CakeNumberRemember" + i, cakeNumberRemember[i]);
                mObject.transform.position = currentPoint.transform.position;
                mObject.GetComponent<CakeItem>().holder = i;
                mObject.transform.SetParent(spawnPoints[i].transform);
                spawnPoints[i].GetComponent<Holder>().cake = mObject;
                PlayerPrefs.SetString("Holder" + i, "Holder" + i.ToString());
                break;
            } 
        }
        if (IsAvailableHolders() && CurrencyManager.Instance.Coins >= price)
        {
            spawnCakeButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            spawnCakeButton.GetComponent<Button>().interactable = false;
        }
    }


    public void SpawnNextCake(int num, Transform mpos, int holder)
    {
        if (num >= cakes.Count)
        {
            num = 0;
        }
        GameObject mObject = Instantiate(cakes[num]);
        mObject.transform.position = mpos.position;
        mObject.transform.SetParent(mpos);
        mpos.GetComponent<Holder>().cake = mObject;
        mObject.GetComponent<CakeItem>().lastPos = mpos.position;
        mObject.GetComponent<CakeItem>().holder = holder;
        cakeExists[holder] = true;
    }


    public void ServeCakes()
    {
        howManyCakesButton.SetActive(false);
        serveButton.gameObject.SetActive(false);
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            cakesInstantiate.Add(gameObject);
        }
        serveButton.gameObject.GetComponent<Button>().interactable = false;
        spawnCakeButton.GetComponent<Button>().interactable = false;
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            cakesInstantiate[i] = Instantiate(spawnPoints[i]);
            for (int j = 0; j < cakesInstantiate[i].gameObject.transform.childCount; j++)
            {
                cakesInstantiate[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            }
            cakesInstantiate[i].GetComponent<Holder>().MoveToBelt();
            points.gameObject.SetActive(false);
        }
        
        cameraController.MoveDestination();
        //StartCoroutine(WaitForEnableCakes());
        
    }
    int rememberNumber;

    public List<GameObject> spawnedCakes = new List<GameObject>();
    GameObject element1;
    GameObject element2;
    public void AutoMergeCakes()
    {
        // //for (int i = 0;i < spawnPoints.Count;i++)
        // //{
        // //    if(i == spawnPoints[i].transform.GetChild(0).transform.GetComponent<CakeItem>().id)
        // //    {
        //         spawnPoints[0].transform.GetChild(0).transform.GetComponent<CakeItem>().transform.DOMove(
        //             spawnPoints[1].transform.GetChild(0).transform.GetComponent<CakeItem>().transform.position,1f);
        ////spawnPoints[0].transform.GetChild(0).transform.GetComponent<CakeItem>().GetComponent<BoxCollider>().isTrigger = false;
        // //spawnPoints[1].transform.GetChild(0).transform.GetComponent<CakeItem>().otherId = 1;
        // //spawnPoints[0].transform.GetChild(0).transform.GetComponent<CakeItem>().NewMergePart(0);
        // //    }
        // //}



        Debug.Log("Auto Merge");


        spawnedCakes.Clear();
        //for(int i = 0; i < spawnPoints.Count; i++)
        //{
        //    if (spawnPoints[i].GetComponent<Holder>().cake)
        //    {
        //        spawnedCakes.Add(spawnPoints[i].GetComponent<Holder>().cake);
        //    }

        //}
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            spawnedCakes.Add(spawnPoints[i]);
        }

        StartCoroutine("AutoMergeRoutine");


    }

    IEnumerator AutoMergeRoutine()
    {
        for (int i = 0; i < spawnedCakes.Count; i++)
        {
            for (int j = i + 1; j < spawnedCakes.Count; j++)
            {
                
                if (spawnedCakes[j].GetComponent<Holder>().cake)
                {
                    element1 = spawnedCakes[j].GetComponent<Holder>().cake;
                }

                if (spawnedCakes[i].GetComponent<Holder>().cake)
                {
                    element2 = spawnedCakes[i].GetComponent<Holder>().cake;
                }
                if(element1 == element2)
                {

                }
                else if (element1 && element2 && element1.GetComponent<CakeItem>().id == element2.GetComponent<CakeItem>().id)
                {
                    element1.transform.DOMove(element2.transform.position, .5f);
                    yield return new WaitForSeconds(.5f);
                    cakeExists[element1.GetComponent<CakeItem>().holder] = false;
                    IsAvailableHolders();
                    int mNum = element1.GetComponent<CakeItem>().id + 1;
                    PlayerPrefs.SetInt("CakeNumberRemember" + i, mNum);
                    SpawnNextCake(mNum, spawnPoints[i].transform, element2.GetComponent<CakeItem>().holder);
                    Destroy(element1);
                    Destroy(element2);
                    Debug.Log(i);
                }
                else
                {
                    Debug.Log("Else");
                }
            }
            yield return new WaitForSeconds(0f);
        }
    }


    public void CameraresetPosition()
    {
        belt1.isServe = false;
        belt2.isServe = false;
        belt3.isServe = false;

        continueButton.gameObject.SetActive(false);
        serveButton.gameObject.GetComponent<Button>().interactable = true;
        spawnCakeButton.GetComponent<Button>().interactable = true;
        
        points.gameObject.SetActive(true);
        
        //cameraController.StartDestination();
        DeactivateCakesInstantiate();
    }
    public void DeactivateCakesInstantiate()
    {
        foreach (var item in cakesInstantiate)
        {
            Destroy(item);
        }
        cakesInstantiate.Clear();
    }
    public void SpawnEffect(GameObject DollerType, Transform target)
    {
        GameObject mObject = Instantiate(DollerType);
        mObject.transform.position = target.position;
    }
    public IEnumerator WaitForEnableCakes()
    {
        yield return new WaitForSeconds(5f);
        continueButton.gameObject.SetActive(true);
    }
    private void OnApplicationQuit()
    {
        SavedGame();
    }
    private void OnApplicationFocus()
    {
        SavedGame();
    }
    public void SavedGame()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (spawnPoints[i] && !cakeExists[i])
            {
                if (PlayerPrefs.GetString("Holder" + i.ToString()) == "Holder" + i.ToString())
                {
                    currentPoint = spawnPoints[i];
                    cakeExists[i] = true;
                    GameObject mObject = Instantiate(cakes[PlayerPrefs.GetInt("CakeNumberRemember" + i)]);
                    mObject.transform.position = currentPoint.transform.position;
                    mObject.GetComponent<CakeItem>().holder = i;
                    mObject.transform.SetParent(spawnPoints[i].transform);
                    spawnPoints[i].GetComponent<Holder>().cake = mObject;
                }
            }
        }
    }
    public void GameContinueButton()
    {
        foreach (var item in CakesManager.instance.cameraController.go)
        {
            Destroy(item.gameObject);
        }
        DeactivateCakesInstantiate();
        CakesManager.instance.levelManager.LevelCount += 1;
        PlayerPrefs.SetInt("Levels", CakesManager.instance.levelManager.LevelCount);
        continueButton.gameObject.SetActive(false);
        CakesManager.instance.levelManager.GetLevel(PlayerPrefs.GetInt("Levels"));
        cameraController.isBackwardMove = true;
        cameraController.isForwardMove = false;
        points.SetActive(true);
        CakesManager.instance.howManyCakesButton.SetActive(true);
        CakesManager.instance.serveButton.gameObject.SetActive(true);
    }
}
