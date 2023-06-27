//#region import
using System.Collections.Generic;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using DG.Tweening;
using System;
//#endregion

public class UiCameraFinder : MonoBehaviour
{
    //#region editors fields and properties
    //#endregion

    //#region public fields and properties
    //#endregion

    //#region private fields and properties

    private Canvas canvas;

    //#endregion


    //#region life-cycle callbacks

    private void Awake()
    {
        canvas = GetComponent<Canvas>();

        if (canvas.worldCamera == null)
        {
            canvas.worldCamera = GameObject.Find("UI Camera").GetComponent<Camera>();
        }
    }

    //#endregion

    //#region public methods
    //#endregion

    //#region private methods
    //#endregion

    //#region event handlers
    //#endregion
}
