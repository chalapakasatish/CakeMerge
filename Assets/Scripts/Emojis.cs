using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Emojis : MonoBehaviour
{

    public List<GameObject> emojisList = new List<GameObject>();
    private int randomNumber;
    // Start is called before the first frame update
    void Start()
    {
        randomNumber = Random.Range(0, emojisList.Count);
        for(int i = 0; i < emojisList.Count; i++)
        {
            if(i == randomNumber)
            {
                emojisList[i].SetActive(true);
                emojisList[i].transform.DOScale(1f, .5f);
            }
            else
            {
                emojisList[i].SetActive(false);
            }
        }

        Destroy(gameObject, 1f);

    }






}
