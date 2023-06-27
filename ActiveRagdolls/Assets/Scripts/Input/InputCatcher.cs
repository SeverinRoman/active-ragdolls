//#region import
using System.Collections.Generic;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;
//#endregion

public class InputCatcher : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerMoveHandler
{
    //#region editors fields and properties
    [SerializeField] private InputCatcherType inputCatcherType;
    //#endregion

    //#region public fields and properties
    //#endregion

    //#region private fields and properties

    private bool isInput = false;

    //#endregion


    //#region life-cycle callbacks

    //#endregion

    //#region public methods
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        GameEventManager.InputCatcherDown?.Invoke(inputCatcherType, pointerEventData);
        // Debug.Log("start");
        isInput = true;
    }

    public void OnPointerMove(PointerEventData pointerEventData)
    {
        if (!isInput) return;
        GameEventManager.InputCatcherMove?.Invoke(inputCatcherType, pointerEventData);
        // Debug.Log("move");
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        GameEventManager.InputCatcherUp?.Invoke(inputCatcherType, pointerEventData);
        // Debug.Log("up");
        isInput = false;
    }

    //#endregion

    //#region private methods
    //#endregion

    //#region event handlers
    //#endregion
}
