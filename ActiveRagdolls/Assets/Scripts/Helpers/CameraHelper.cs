//#region import
using System.Collections.Generic;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using DG.Tweening;
using System;
//#endregion


[RequireComponent(typeof(Camera))]
public class CameraHelper : MonoBehaviour
{
    //#region editors fields and properties

    [SerializeField] private CameraType cameraType;

    //#endregion

    //#region public fields and properties
    //#endregion

    //#region private fields and properties

    private new Camera camera;

    //#endregion


    //#region life-cycle callbacks

    void Awake()
    {
        camera = GetComponent<Camera>();
    }

    void OnEnable()
    {
        GameEventManager.GetCamera.AddListener(OnGetCamera);
    }

    void OnDisable()
    {
        GameEventManager.GetCamera.RemoveListener(OnGetCamera);
    }

    //#endregion

    //#region public methods
    //#endregion

    //#region private methods

    private Camera GetCamera()
    {
        return camera;
    }

    //#endregion

    //#region event handlers

    protected void OnGetCamera(CameraType cameraType, Action<Camera> callback)
    {
        if (this.cameraType == cameraType)
        {
            callback(GetCamera());
        }
    }

    //#endregion
}
