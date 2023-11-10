using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class PieceMove : MonoBehaviour
{
    public void MovePiece(Transform target)
    {
        transform.DOLocalMove(target.localPosition, .3f).OnComplete(()=> FinishCallBack());
        transform.DOScale(0f, 1f);
    }
    

    public void FinishCallBack()
    {
        Destroy(gameObject,3f);
        CakesManager.instance.SpawnEffect(CakesManager.instance.dollerEffect, transform);
        CurrencyManager.Instance.AddCurrency(10);
        //GameObject emoji = Instantiate(CakesManager.instance.emojis);
        //emoji.transform.position = transform.position + new Vector3(0, 1, 0);

    }
    public void MovePieceFinal(Transform target)
    {
        transform.DOLocalMove(target.localPosition, .3f).OnComplete(() => FinishCallBackFinalGuy());
        transform.DOScale(0f, 1f);
    }

    bool ate;
    public void FinishCallBackFinalGuy()
    {
        Destroy(gameObject, 3f);
        CakesManager.instance.SpawnEffect(CakesManager.instance.dollerEffect, transform);
        CurrencyManager.Instance.AddCurrency(100);
        
        //if (ate == false)
        //{
        //    Debug.Log("Last");
        //    GameObject emoji = Instantiate(CakesManager.instance.emojis);
        //    emoji.transform.position = transform.position + new Vector3(0, 1, 0);
        //    ate = true;
        //}
    }
}
