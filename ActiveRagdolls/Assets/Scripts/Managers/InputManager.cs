//#region import
using System.Collections.Generic;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;
//#endregion

public class InputManager : MonoBehaviour
{
    //#region editors fields and properties
    [SerializeField] private LayerMask selectMask;
    [SerializeField] private bool isOnInput = true;


    //#endregion

    //#region public fields and properties
    //#endregion

    //#region private fields and properties
    private new Camera camera;

    [SerializeField] private ISelectable selectable;

    //#endregion


    //#region life-cycle callbacks

    void Awake()
    {
        camera = GetCamera();
    }

    void OnEnable()
    {
        GameEventManager.InputCatcherDown.AddListener(OnInputCatcherDown);
        GameEventManager.InputCatcherMove.AddListener(OnInputCatcherMove);
        GameEventManager.InputCatcherUp.AddListener(OnInputCatcherUp);

        GameEventManager.ToggleInput.AddListener(OnToggleInput);
    }

    void OnDisable()
    {
        GameEventManager.InputCatcherDown.RemoveListener(OnInputCatcherDown);
        GameEventManager.InputCatcherMove.RemoveListener(OnInputCatcherMove);
        GameEventManager.InputCatcherUp.RemoveListener(OnInputCatcherUp);

        GameEventManager.ToggleInput.RemoveListener(OnToggleInput);
    }

    //#endregion

    //#region public methods
    //#endregion

    //#region private methods

    private GameObject ClickSelect(Vector3 position)
    {
        Ray ray = camera.ScreenPointToRay(position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f, selectMask))
        {
            Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.green);
            if (hit.collider != null)
            {
                float distance = Vector3.Distance(camera.transform.position, hit.point);
                Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
                return hit.collider.gameObject;
            }
        }
        return null;
    }

    private void StartInput(PointerEventData pointerEventData)
    {
        GameObject inputObject = ClickSelect(pointerEventData.position);
        if (inputObject == null) return;

        if (inputObject.TryGetComponent(out ISelectable isSelectable))
        {
            selectable = isSelectable;
            isSelectable.Selected(camera.ScreenToWorldPoint(pointerEventData.position));
        }
    }

    private void MoveInput(PointerEventData pointerEventData)
    {
        if (selectable == null) return;
    }

    private void EndInput(PointerEventData pointerEventData)
    {
        selectable = null;
        GameEventManager.SelectChooseEnd?.Invoke();
    }

    private Camera GetCamera()
    {
        Camera _camera = null;
        Action<Camera> callback = (a) =>
        {
            _camera = a;
        };

        GameEventManager.GetCamera.Invoke(CameraType.Main, callback);

        return _camera;
    }


    //#endregion

    //#region event handlers

    protected void OnToggleInput(bool isOn)
    {
        isOnInput = isOn;
    }

    protected void OnInputCatcherDown(InputCatcherType inputCatcherType, PointerEventData pointerEventData)
    {
        if (InputCatcherType.GameInput == inputCatcherType)
        {
            StartInput(pointerEventData);
        }
    }

    protected void OnInputCatcherMove(InputCatcherType inputCatcherType, PointerEventData pointerEventData)
    {
        if (InputCatcherType.GameInput == inputCatcherType)
        {
            MoveInput(pointerEventData);
        }
    }

    protected void OnInputCatcherUp(InputCatcherType inputCatcherType, PointerEventData pointerEventData)
    {
        if (InputCatcherType.GameInput == inputCatcherType)
        {
            EndInput(pointerEventData);
        }
    }

    //#endregion
}
