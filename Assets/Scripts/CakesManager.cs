using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Button spawnCakeButton,continueButton,serveButton;
    public List<GameObject> cakesInstantiate = new List<GameObject>();
    public GameObject points;
    [SerializeField]private int price;

    public int Price { get => price; set => price = value; }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        price = PlayerPrefs.GetInt("PriceCake", price);
    }
    public void SpawnCake()
    {
        if (IsAvailableHolders())
        {
            GetCake(0);
        }
        
    }
    bool IsAvailableHolders()
    {
        for (int i = 0; i < 9; i++)
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
        Price += 1;
        PlayerPrefs.SetInt("PriceCake", Price);
        CurrencyManager.Instance.PriceCakeText.text = PlayerPrefs.GetInt("PriceCake").ToString();
        if (CurrencyManager.Instance.Coins <= 0)
        {
            spawnCakeButton.GetComponent<Button>().interactable = false;
        }
        
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if(spawnPoints[i] && !cakeExists[i])
            {
                currentPoint = spawnPoints[i];
                cakeExists[i] = true;
                GameObject mObject = Instantiate(cakes[num]);
                mObject.transform.position = currentPoint.transform.position;
                mObject.GetComponent<CakeItem>().holder = i;
                mObject.transform.SetParent(spawnPoints[i].transform);
                spawnPoints[i].GetComponent<Holder>().cake = mObject;
                break;
            } 
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
        serveButton.gameObject.GetComponent<Button>().interactable = false;
        spawnCakeButton.GetComponent<Button>().interactable = false;
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            cakesInstantiate[i] = Instantiate(spawnPoints[i]);
            cakesInstantiate[i].GetComponent<Holder>().MoveToBelt();
            points.gameObject.SetActive(false);
        }
        StartCoroutine(WaitForEnableCakes());
        cameraController.MoveDestination();
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
        cameraController.StartDestination();
    }

    public void SpawnEffect(GameObject DollerType, Transform target)
    {
        GameObject mObject = Instantiate(DollerType);
        mObject.transform.position = target.position;
    }
    public IEnumerator WaitForEnableCakes()
    {
        yield return new WaitForSeconds(10f);
        continueButton.gameObject.SetActive(true);
    }
}
