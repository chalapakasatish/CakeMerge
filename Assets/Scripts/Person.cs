using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public Transform[] mouthPoint;
    public int count;
    [SerializeField] private int ate;
    public int Ate { get { return ate; } set { ate = value; } }
    private void Awake()
    {
        ate = count;
    }
}
