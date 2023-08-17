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
        StartCoroutine(ChangeCameraFOV(80, 5));
        transform.DOLocalRotate(new Vector3(60, 0, 0),1f);
        transform.DOMoveZ(destination.localPosition.z, 5);
    }
    public void StartDestination()
    {
        StartCoroutine(ChangeCameraFOV(60, 1));
        transform.DOLocalRotate(new Vector3(50, 0, 0), 1f);
        transform.DOMoveZ(startPosition.localPosition.z, 1);
    }



    public Camera targetCamera;
    public float targetFOV = 70f;
    public float changeDuration = 5f;

    private float initialFOV;

    private void Start()
    {
        initialFOV = targetCamera.fieldOfView;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ChangeCameraFOV(targetFOV, changeDuration));
        }
    }

    private IEnumerator ChangeCameraFOV(float targetFOV, float duration)
    {
        float elapsedTime = 0f;
        float startFOV = targetCamera.fieldOfView;

        while (elapsedTime < duration)
        {
            float newFOV = Mathf.Lerp(startFOV, targetFOV, elapsedTime / duration);
            targetCamera.fieldOfView = newFOV;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        targetCamera.fieldOfView = targetFOV; // Ensure the final FOV value is set accurately
    }
}

