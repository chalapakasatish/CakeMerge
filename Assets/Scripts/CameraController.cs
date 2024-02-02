using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Transform destination;
    public Transform startPosition;
    public bool isForwardMove = false,isBackwardMove = false;
    public void MoveDestination()
    {
        StartCoroutine(WaitForCameraMovementStart());
         
        //StartCoroutine(ChangeCameraFOV(60, 5));
        //transform.DOLocalRotate(new Vector3(60, 0, 0),1f);
        //transform.DOMoveZ(destination.localPosition.z, 8);
        //transform.position += destination.position;
    }
    public IEnumerator WaitForCameraMovementStart()
    {
        yield return new WaitForSeconds(0.2f);
        isForwardMove = true;
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
        if (isForwardMove)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(destination.position.x, destination.position.y, destination.position.z), 0.1f * Time.deltaTime);
        }
        else if(isBackwardMove)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(startPosition.position.x, startPosition.position.y, startPosition.position.z), 3f * Time.deltaTime);
            CakesManager.instance.CheckTargetClicks();
        }


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
                    CakesManager.instance.serveStarted = false;
                    isForwardMove = false;
                    isBackwardMove = true;
                    CakesManager.instance.howManyCakesButton.SetActive(true);
                    CakesManager.instance.serveButton.gameObject.SetActive(true);
                    CakesManager.instance.autoMergeButton.gameObject.SetActive(true);
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

