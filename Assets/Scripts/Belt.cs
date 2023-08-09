using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{

    private float scrollSpeed = -0.2f;
    Renderer rend;
   public bool isServe;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }
    float offset;
    void Update()
    {
        if (isServe)
        {
            offset -= Time.deltaTime * scrollSpeed;
            rend.material.mainTextureOffset = new Vector2(0, offset);
        }
    }
}
