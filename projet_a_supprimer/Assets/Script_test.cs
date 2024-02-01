using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_test : MonoBehaviour
{

    enum WeekDays
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
    enum OrderStatus
    {
        WaitingValidation,
        Validated,
        WaitingPayment,
        Paid,
        Shipped,
        Refunded ,
        Canceled
    }


    [SerializeField] private int waitSecond;
    private void Start()
    {
        StartCoroutine(MyCoroutine(waitSecond));

        foreach(WeekDays day in Enum.GetValues(typeof(WeekDays)))
        {
            Debug.Log(day);
        }
    }

    IEnumerator MyCoroutine(int waitSecond)
    {
        print("MyCoroutine commence son exécution.");
        yield return new WaitForSeconds(waitSecond);
        print("MyCoroutine a terminée son exécution. ");
    }
}
