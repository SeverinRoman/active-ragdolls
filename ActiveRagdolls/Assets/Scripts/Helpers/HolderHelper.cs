//#region import
using System.Collections.Generic;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using DG.Tweening;
using System;
//#endregion



public class HolderHelper : MonoBehaviour
{
    //#region editors fields and properties

    [SerializeField] private HolderType holderType;

    //#endregion

    //#region public fields and properties
    //#endregion

    //#region private fields and properties

    //#endregion


    //#region life-cycle callbacks

    void OnEnable()
    {
        GameEventManager.GetHolder.AddListener(OnGetHolder);
    }

    void OnDisable()
    {
        GameEventManager.GetHolder.RemoveListener(OnGetHolder);
    }

    //#endregion

    //#region public methods
    //#endregion

    //#region private methods

    private Transform GetHolder()
    {
        return transform;
    }

    //#endregion

    //#region event handlers

    protected void OnGetHolder(HolderType holderType, Action<Transform> callback)
    {
        if (this.holderType == holderType)
        {
            callback(GetHolder());
        }
    }

    //#endregion
}
