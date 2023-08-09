using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public Transform destination;
    public Transform startPosition;


    public void MoveDestination()
    {
        transform.DOMoveZ(destination.localPosition.z, 15);
    }
    public void StartDestination()
    {
        transform.DOMoveZ(startPosition.localPosition.z, 5);
    }
}
