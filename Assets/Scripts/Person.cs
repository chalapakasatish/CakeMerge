using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Person : MonoBehaviour
{
    public Transform[] mouthPoint;
    public TMP_Text countText;
    public int count;
    [SerializeField] private int ate;
    private float countdown = 2f;
    //private Renderer mr;

    public int Ate { get { return ate; } set { ate = value; } }
    private void Awake()
    {
        //mr = transform.GetChild(0).GetChild(0).GetChild(0).transform.GetComponent<Renderer>();
        //mr.material.color = new Color(0f, 1f, 1f, 1f);
        ate = count;
        countText.text = ate.ToString();
    }
    private void Update()
    {
        if(ate <= 0)
        {
            if (countdown <= 0)
            {
                ate = count;
                countText.text = ate.ToString();
                GetComponent<Collider>().enabled = true;
                countdown = 2f;
            }
            else
            {
                countdown -= Time.deltaTime;
            }
        }
    }
}
