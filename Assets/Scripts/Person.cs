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
    private float countdown = 3f;

    public int Ate { get { return ate; } set { ate = value; } }
    private void Awake()
    {
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
                countdown = 3f;
            }
            else
            {
                countdown -= Time.deltaTime;
            }
        }
    }
}
