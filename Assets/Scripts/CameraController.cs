using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Transform destination;
    public Transform startPosition;
    public void MoveDestination()
    {
        //StartCoroutine(ChangeCameraFOV(60, 5));
        //transform.DOLocalRotate(new Vector3(60, 0, 0),1f);
        transform.DOMoveZ(destination.localPosition.z, 8);
        //transform.position = Vector3.Lerp(transform.position,new Vector3( destination.position.x, destination.position.y, destination.position.z), 5f);
        //transform.position += destination.position;
    }
    public void StartDestination()
    {
        StartCoroutine(ChangeCameraFOV(60, 1));
        transform.DOLocalRotate(new Vector3(50, 0, 0), 1f);
        transform.DOMoveZ(startPosition.localPosition.z, 45f);
    }

    public Camera targetCamera;
    public float targetFOV = 70f;
    public float changeDuration = 5f;

    private float initialFOV;
    public GameObject[] go;
    private void Start()
    {
        initialFOV = targetCamera.fieldOfView;
    }
    private void Update()
    {
        if (CakesManager.instance.serveStarted == true)
        {
            go = GameObject.FindGameObjectsWithTag("Plate");
            for (int i = 0; i <= go.Length; i++)
            {
                if (go.Length == 0)
                {
                    CakesManager.instance.belt1.isServe = false;
                    CakesManager.instance.belt2.isServe = false;
                    CakesManager.instance.belt3.isServe = false;
                    CakesManager.instance.serveButton.gameObject.GetComponent<Button>().interactable = true;
                    CakesManager.instance.spawnCakeButton.GetComponent<Button>().interactable = true;
                    CakesManager.instance.points.gameObject.SetActive(true);
                    CakesManager.instance.DeactivateCakesInstantiate();
                    //transform.DOLocalRotate(new Vector3(50, 0, 0), 1f);
                    transform.DOMoveZ(startPosition.localPosition.z, 4);
                    //transform.position = Vector3.Lerp(transform.position, startPosition.position, 1f);
                    CakesManager.instance.serveStarted = false;
                }
            }
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

