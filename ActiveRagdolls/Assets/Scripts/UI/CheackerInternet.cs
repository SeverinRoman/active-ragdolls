//#region import
using System.Collections.Generic;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using DG.Tweening;
using System;
//#endregion

public class CheackerInternet : MonoBehaviour
{
    //#region editors fields and properties
    [SerializeField] private float delayCheack = 30f;
    [SerializeField] private float delayCheackNoInternet = 10f;
    [SerializeField] private bool isDebug = false;
    [SerializeField] private GameObject holder;


    //#endregion

    //#region public fields and properties
    //#endregion

    //#region private fields and properties
    bool isStopTime = false;
    Tween delay = null;

    //#endregion


    //#region life-cycle callbacks

    void Start()
    {
#if !UNITY_EDITOR
        isDebug = false;
#endif
        if (isDebug) return;

        CheackStandart();
    }

    //#endregion

    //#region public methods
    //#endregion

    //#region private methods

    private bool CheackInternet()
    {
        Debug.Log("Check");
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void StopTime(bool isStop)
    {
        if (isStopTime == isStop) return;

        if (isStop)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        isStopTime = isStop;
    }


    private void CheackStandart()
    {
        if (delay != null) delay.Kill();

        int empty = 0;
        delay = DOTween.To(() => empty, x => empty = x, 0, delayCheack);
        delay.SetUpdate(true);
        delay.SetLink(gameObject);
        delay.OnStepComplete(() =>
        {
            bool onInternet = CheackInternet();
            StopTime(!onInternet);
            holder.SetActive(!onInternet);

            if (!onInternet)
            {
                CheackFast();
            }
        });
        delay.SetLoops(-1);
    }

    private void CheackFast()
    {
        if (delay != null) delay.Kill();

        int empty = 0;
        delay = DOTween.To(() => empty, x => empty = x, 0, delayCheackNoInternet);
        delay.SetUpdate(true);
        delay.SetLink(gameObject);
        delay.OnStepComplete(() =>
        {
            bool onInternet = CheackInternet();
            StopTime(!onInternet);
            holder.SetActive(!onInternet);

            if (onInternet)
            {
                CheackStandart();
            }
        });
        delay.SetLoops(-1);
    }

    //#endregion

    //#region event handlers
    //#endregion
}
