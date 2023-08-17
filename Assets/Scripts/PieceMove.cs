using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PieceMove : MonoBehaviour
{
    public void MovePiece(Transform target)
    {
        transform.DOLocalMove(target.localPosition, .3f).OnComplete(()=> FinishCallBack());
        transform.DOScale(.1f, 1f);
        Destroy(gameObject,2f);
    }
    

    public void FinishCallBack()
    {
        GetComponent<MeshRenderer>().enabled = false;
        CakesManager.instance.SpawnEffect(CakesManager.instance.dollerEffect, transform);
        CurrencyManager.Instance.AddCurrency(7);
        GameObject emoji = Instantiate(CakesManager.instance.emojis);
        emoji.transform.position = transform.position + new Vector3(0, 1, 0);
    }
}
