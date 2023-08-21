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
    IEnumerator DistributePieces(Collider other, int wantCakes)
    {
        for (int i = 0; i < wantCakes; i++)
        {
            if (0 < cakePieces.Count && other.GetComponent<Person>().Ate >= 0)
            {
                cakePieces[0].transform.SetParent(other.transform);
                cakePieces[0].AddComponent<PieceMove>();
                cakePieces[0].GetComponent<PieceMove>().MovePiece(other.GetComponent<Person>().mouthPoint[0]);
                cakePieces.RemoveAt(0);
                other.GetComponent<Person>().Ate -= 1;
            }
            if (other.GetComponent<Person>().Ate <= 0)
            {
                other.GetComponent<Collider>().enabled = false;
            }
            if (cakePieces.Count <= 0)
            {
                GetComponent<Collider>().enabled = false;
                Invoke("DeativatePlate", 0.2f);
            }
        }


        yield return new WaitForSeconds(3f);
        other.GetComponent<Collider>().enabled = true;
        other.GetComponent<Person>().Ate = other.GetComponent<Person>().count;
    }
    public void MoveAllPieces(GameObject other)
    {
        for(int i = 0; i < cakePieces.Count; i++)
        {
            if (cakePieces[i] == null)
            {
                Debug.Log("Some Pieces Deleted");
            }else
            {
                cakePieces[i].AddComponent<PieceMove>();
                cakePieces[i].GetComponent<PieceMove>().MovePiece(other.GetComponent<StageTrigger>().person[0].transform);
            }
            
        }
    }

    void DeativatePlate()
    {
        Destroy(plate);
    }



    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Person":
                StartCoroutine(DistributePieces(other, other.GetComponent<Person>().Ate));
                break;
            //case "stage1":
            //    StartCoroutine(DistributePieces(other));
            //    break;
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
