//#region import
using System.Collections.Generic;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using DG.Tweening;
using System;
//#endregion


[RequireComponent(typeof(Canvas))]
public class CanvasHelper : MonoBehaviour
{
    //#region editors fields and properties

    [SerializeField] private CanvasType canvasType;
    [SerializeField] private CameraType cameraType;

    //#endregion

    //#region public fields and properties
    //#endregion

    //#region private fields and properties

    private Canvas canvas;

    //#endregion


    //#region life-cycle callbacks

    void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = GetCamera(cameraType);
    }

    void OnEnable()
    {
        GameEventManager.GetCanvas.AddListener(OnGetCanvas);
        GameEventManager.GetPositionRelativelyCanvas.AddListener(OnGetPositionRelativelyCanvas);
        GameEventManager.GetResolutionOffset.AddListener(OnGetResolutionOffset);
    }

    void OnDisable()
    {
        GameEventManager.GetCanvas.RemoveListener(OnGetCanvas);
        GameEventManager.GetPositionRelativelyCanvas.RemoveListener(OnGetPositionRelativelyCanvas);
        GameEventManager.GetResolutionOffset.RemoveListener(OnGetResolutionOffset);
    }

    // void Start()
    // {

    // }

    //#endregion

    //#region public methods
    //#endregion

    //#region private methods

    private Canvas GetCanvas()
    {
        return canvas;
    }

    private Vector3 GetPositionRelativelyCanvas(CameraType cameraType, Vector3 position)
    {
        Vector3 newPosition = GetCamera(cameraType).WorldToViewportPoint(position);
        Vector2 resolution = new Vector2(canvas.GetComponent<RectTransform>().sizeDelta.x, canvas.GetComponent<RectTransform>().sizeDelta.y);
        newPosition = new Vector3((resolution.x * newPosition.x), (resolution.y * newPosition.y), 0f);
        newPosition -= (Vector3)resolution / 2;
        return newPosition;
    }

    private Vector3 GetResolutionOffset(Vector3 position)
    {
        Vector2 resolution = new Vector2(canvas.GetComponent<RectTransform>().sizeDelta.x, canvas.GetComponent<RectTransform>().sizeDelta.y);
        position -= (Vector3)resolution / 2;

        return position;
    }

    private Camera GetCamera(CameraType cameraType)
    {
        Camera _camera = null;
        Action<Camera> callback = (a) =>
        {
            _camera = a;
        };

        GameEventManager.GetCamera.Invoke(cameraType, callback);

        return _camera;
    }

    //#endregion

    //#region event handlers

    protected void OnGetCanvas(CanvasType canvasType, Action<Canvas> callback)
    {
        if (this.canvasType == canvasType)
        {
            callback(GetCanvas());
        }
    }

    protected void OnGetPositionRelativelyCanvas(Vector3 position, CameraType cameraType, CanvasType canvasType, Action<Vector3> callback)
    {
        if (this.canvasType == canvasType)
        {
            callback(GetPositionRelativelyCanvas(cameraType, position));
        }
    }

    protected void OnGetResolutionOffset(CanvasType canvasType, Vector3 position, Action<Vector3> callback)
    {
        if (this.canvasType == canvasType)
        {
            callback(GetResolutionOffset(position));
        }
    }

    //#endregion
}
