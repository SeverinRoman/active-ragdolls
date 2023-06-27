//#region import
using System.Collections.Generic;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using DG.Tweening;
using System;
//#endregion




public class UIManager : MonoBehaviour
{
    [Serializable]
    private class ScreenConfig
    {
        public UIScreenType type = UIScreenType.None;
        public GameObject gameObject;
        public bool isStart;
    }

    //#region editors fields and properties
    [SerializeField] private List<ScreenConfig> screens = new List<ScreenConfig>();
    //#endregion

    //#region public fields and properties
    //#endregion

    //#region private fields and properties
    private new Camera camera;
    private RectTransform canvasRect;
    //#endregion


    //#region life-cycle callbacks
    void Awake()
    {
        Init();
    }

    void OnEnable()
    {
        GameEventManager.TransferToUiScreen.AddListener(OnTransferToUiScreen);
        GameEventManager.TransferToWorld.AddListener(OnTransferToWorld);
        GameEventManager.ToggleScreen.AddListener(OnToggleScreen);
    }

    void OnDisable()
    {
        GameEventManager.TransferToUiScreen.RemoveListener(OnTransferToUiScreen);
        GameEventManager.TransferToWorld.RemoveListener(OnTransferToWorld);
        GameEventManager.ToggleScreen.RemoveListener(OnToggleScreen);
    }

    //#endregion

    //#region public methods
    //#endregion

    //#region private methods

    private void Init()
    {

        camera = GetCamera();
        canvasRect = GetCanvas().GetComponent<RectTransform>();

        foreach (ScreenConfig screen in screens)
        {
            if (screen.isStart)
            {
                ToggleScreen(screen.type, true);
            }
            else
            {
                ToggleScreen(screen.type, false);
            }
        }
    }

    private void ToggleScreen(UIScreenType type, bool isOn)
    {
        foreach (ScreenConfig screen in screens)
        {
            if (screen.type == type)
            {
                screen.gameObject.SetActive(isOn);
                break;
            }
        }
    }

    private void TransferToUiScreen(UIScreenType type, GameObject gameObject)
    {
        ScreenConfig screen = screens.Find(i => i.type == type);

        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();

        if (rectTransform == null)
        {
            rectTransform = gameObject.AddComponent<RectTransform>();
        }

        Vector3 startPosition = rectTransform.anchoredPosition3D;
        gameObject.transform.SetParent(screen.gameObject.transform);

        rectTransform.anchoredPosition3D = GetWorldPosition(gameObject.transform.position) + startPosition;
        gameObject.layer = (int)LayerType.UI;
    }

    private void TransferToWorld(GameObject gameObject, Transform parent)
    {
        Vector3 scale = gameObject.transform.localScale;

        Vector3 startPosition = Vector3.zero;
        gameObject.transform.SetParent(parent);

        gameObject.layer = (int)LayerType.Default;
    }

    private Vector3 GetWorldPosition(Vector3 position)
    {
        Vector3 newPositionAnnouncer = camera.WorldToViewportPoint(position);
        Vector2 resolution = new Vector2(canvasRect.sizeDelta.x, canvasRect.sizeDelta.y);
        newPositionAnnouncer = new Vector3((resolution.x * newPositionAnnouncer.x), (resolution.y * newPositionAnnouncer.y), 0f);
        newPositionAnnouncer -= (Vector3)resolution / 2;
        return newPositionAnnouncer;
    }

    private Camera GetCamera()
    {
        Camera camera = null;
        Action<Camera> callback = (a) =>
        {
            camera = a;
        };

        GameEventManager.GetCamera.Invoke(CameraType.Main, callback);

        return camera;
    }

    private Canvas GetCanvas()
    {
        Canvas canvas = null;
        Action<Canvas> callback = (a) =>
        {
            canvas = a;
        };

        GameEventManager.GetCanvas.Invoke(CanvasType.UI, callback);

        return canvas;
    }
    //#endregion

    //#region event handlers

    protected void OnToggleScreen(UIScreenType type, bool isOn)
    {
        ToggleScreen(type, isOn);
    }

    protected void OnTransferToUiScreen(UIScreenType type, GameObject gameObject)
    {
        TransferToUiScreen(type, gameObject);
    }

    protected void OnTransferToWorld(GameObject gameObject, Transform parent)
    {
        TransferToWorld(gameObject, parent);
    }

    //#endregion
}
