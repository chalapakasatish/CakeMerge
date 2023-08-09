using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Holder : MonoBehaviour
{
    public GameObject cake;

    public enum TypeOfColomn
    {
        first, second, third
    }
    [SerializeField]
    private TypeOfColomn typeOfColomn;
    [SerializeField]
    private Transform beltPoint;
    [SerializeField]
    private Transform endPoint;
    [SerializeField]
    private float tweenSpeed;
    private float tweenEndSpeed;


    public enum TypeOfRow
    {
        first, second, third
    }
    [SerializeField]
    private TypeOfRow typeOfRow;


    public void MoveToBelt()
    {
        if (cake)
        {
            switch (typeOfRow)
            {
                case TypeOfRow.first:
                    tweenSpeed = .4f;
                    tweenEndSpeed = .5f;
                    CakesManager.instance.belt1.isServe = true;
                    break;
                case TypeOfRow.second:
                    tweenSpeed = .6f;
                    tweenEndSpeed = 2.5f;
                    CakesManager.instance.belt2.isServe = true;
                    break;
                case TypeOfRow.third:
                    tweenSpeed = .8f;
                    tweenEndSpeed = 4.5f;
                    CakesManager.instance.belt3.isServe = true;
                    break;
            }

            cake.transform.DOMove(beltPoint.position, tweenSpeed).OnComplete(() => cake.transform.DOMove(endPoint.position, 10f));
            cake.GetComponent<CakeItem>().tablePersons = beltPoint.parent.GetComponent<TablePersons>();
        }
    }


}