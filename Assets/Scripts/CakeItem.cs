using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class CakeItem : MonoBehaviour
{
    public int id;
    public int holder;
    public Vector3 offset;
    public float speed;

    public Vector3 lastPos;

    public List<GameObject> cakePieces = new List<GameObject>();
    public TablePersons tablePersons;

    public GameObject plate;

    public Vector3 Exoffset;

    public bool isDragging = false;


    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
        transform.DOScale(2.5f, .1f);
    }
    //private void Update()
    //{
    //    //Touch Controls
    //    if (Input.touchCount > 0)
    //    {
    //        touch = Input.GetTouch(0);
    //        if (touch.phase == TouchPhase.Began)
    //        {
    //            offset = gameObject.transform.position - GetMouseWorldPos();
    //            isDragging = true;
    //        }
    //        else if (touch.phase == TouchPhase.Moved)
    //        {
    //            transform.position = new Vector3(Mathf.Clamp((GetMouseWorldPos().x + offset.x), -3f, 3f), transform.position.y, Mathf.Clamp((GetMouseWorldPos().z + offset.z), -2f, 2f));
    //        }
    //        else if (touch.phase == TouchPhase.Ended)
    //        {
    //            MergePart();
    //            transform.DOMove(lastPos, .2f);
    //            CakesManager.instance.SpawnEffect(CakesManager.instance.holderEffect, transform);
    //            isDragging = false;
    //        }
    //    }
    //}

    private void OnMouseDown()
    {
        offset = gameObject.transform.position - GetMouseWorldPos();
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        transform.position = new Vector3(Mathf.Clamp((GetMouseWorldPos().x + offset.x), -3f, 3f), transform.position.y, Mathf.Clamp((GetMouseWorldPos().z + offset.z), -2f, 2f));
    }

    private void OnMouseUp()
    {
        MergePart();
        transform.DOMove(lastPos, .2f);
        CakesManager.instance.SpawnEffect(CakesManager.instance.holderEffect, transform);
        isDragging = false;
    }

    int otherId;
    public void MergePart()
    {
        for(int i = 0; i < CakesManager.instance.spawnPoints.Count; i++)
        {
            float dis = Vector3.Distance(CakesManager.instance.spawnPoints[i].transform.position, transform.position);
            
            if (dis <= .8f && i != holder)
            {
                if (CakesManager.instance.spawnPoints[i].GetComponent<Holder>().cake)
                {
                    otherId = CakesManager.instance.spawnPoints[i].GetComponent<Holder>().cake.GetComponent<CakeItem>().id;
                    
                    if (id == otherId)
                    {
                        lastPos = CakesManager.instance.spawnPoints[i].transform.position;
                        CakesManager.instance.cakeExists[holder] = false;
                        PlayerPrefs.DeleteKey("Holder" + holder);
                        CakesManager.instance.cakeExists[i] = true;
                        transform.SetParent(CakesManager.instance.spawnPoints[i].transform);
                        CakesManager.instance.spawnPoints[holder].GetComponent<Holder>().cake = null;
                        CakesManager.instance.spawnPoints[i].GetComponent<Holder>().cake = gameObject;
                        holder = i;
                        int mNum = id + 1;
                        PlayerPrefs.SetInt("CakeNumberRemember" + i, mNum);
                        CakesManager.instance.SpawnNextCake(mNum, CakesManager.instance.spawnPoints[holder].transform, holder);
                        Destroy(CakesManager.instance.spawnPoints[i].transform.GetChild(0).gameObject);
                        Destroy(CakesManager.instance.spawnPoints[i].transform.GetChild(1).gameObject);
                        PlayerPrefs.SetString("Holder" + i, "Holder" + i.ToString());
                    }
                }
                else
                {
                    lastPos = CakesManager.instance.spawnPoints[i].transform.position;
                    CakesManager.instance.cakeExists[holder] = false;
                    PlayerPrefs.DeleteKey("Holder" + holder);
                    CakesManager.instance.cakeExists[i] = true;
                    transform.SetParent(CakesManager.instance.spawnPoints[i].transform);
                    CakesManager.instance.spawnPoints[holder].GetComponent<Holder>().cake = null;
                    CakesManager.instance.spawnPoints[i].GetComponent<Holder>().cake = gameObject;
                    holder = i;
                    PlayerPrefs.SetString("Holder" + i, "Holder" + i.ToString());
                }
            }
            
        }
    }



    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition * speed;
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }


    public List<GameObject> mouthPoints = new List<GameObject>();
    public List<bool> alreadyTook = new List<bool>();


    public List<bool> completedPieces = new List<bool>();
    private Touch touch;

    IEnumerator  DistributePieces(Collider other)
    {
        for (int i = 0; i < other.transform.childCount; i++)
        {
            mouthPoints.Add(other.transform.GetChild(i).gameObject);
            if (mouthPoints.Count >= cakePieces.Count)
            {
                break;
            }
        }
        for (int i = 0; i < mouthPoints.Count; i++)
        {
            if (cakePieces[i])
            {
                cakePieces[i].transform.SetParent(other.transform);
                cakePieces[i].AddComponent<PieceMove>();
                cakePieces[i].GetComponent<PieceMove>().MovePiece(mouthPoints[i].transform);

                //if (i >= other.GetComponent<StageTrigger>().person.Count - 1)
                //{
                    other.GetComponent<Collider>().enabled = false;
                //}
                if (i >= cakePieces.Count - 1)
                {
                    other.GetComponent<Collider>().enabled = false;
                    GetComponent<Collider>().enabled = false;
                    Invoke("DeativatePlate", 0.5f);
                }
            }
        }
        yield return new WaitForSeconds(5f);
        other.GetComponent<Collider>().enabled = true;
        foreach (var item in mouthPoints)
        {
            //Destroy(item);
        }
        mouthPoints.Clear();
        //GetComponent<Collider>().enabled = true;
        //for (int i = 0; i < mouthPoints.Count; i++)
        //{
        //    cakePieces.RemoveAt(i);
        //    mouthPoints.RemoveAt(i);
        //}
    }


    public void MoveAllPieces(GameObject other)
    {
        for(int i = 0; i < cakePieces.Count; i++)
        {
            cakePieces[i].AddComponent<PieceMove>();
            cakePieces[i].GetComponent<PieceMove>().MovePiece(other.GetComponent<StageTrigger>().person[0].transform);
        }
    }

    void DeativatePlate()
    {
        plate.SetActive(false);
    }



    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "stage1":
                StartCoroutine(DistributePieces(other));
                break;
            case "Gift":
                Destroy(other.gameObject);
                CakesManager.instance.SpawnEffect(CakesManager.instance.confettiEffect, other.transform);
                break;
            case "FinalPerson":
                MoveAllPieces(other.gameObject);
                break;
            case "Multiplier":
                other.gameObject.SetActive(false);
                GameObject mObject = Instantiate(gameObject);
                mObject.transform.position = transform.position + new Vector3(0, 1f, 0);
                mObject.transform.SetParent(transform.parent);
                mObject.transform.localRotation = Quaternion.Euler(transform.localEulerAngles);
                //mObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
                mObject.transform.DOMoveZ(CakesManager.instance.destination.transform.localPosition.z, 5);
                break;

        }
    }

}
