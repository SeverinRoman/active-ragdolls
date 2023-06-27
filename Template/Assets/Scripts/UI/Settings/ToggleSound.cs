//#region import
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
//#endregion

public class ToggleSound : ToggleUI
{
    //#region editors fields and properties
    //#endregion

    //#region public fields and properties
    //#endregion

    //#region private fields and properties
    //#endregion


    //#region life-cycle callbacks

    private void OnEnable()
    {
        int empty = 0;
        DOTween.To(() => empty, x => empty = x, 1, 0f).SetLink(gameObject).OnComplete(() =>
            {
                if (Load().onAudio)
                {
                    on.SetActive(true);
                    off.SetActive(false);
                }
                else
                {
                    on.SetActive(false);
                    off.SetActive(true);
                }
                this.isOn = Load().onAudio;
            });
    }

    //#endregion

    //#region public methods

    public override void ToggleOn()
    {
        base.ToggleOn();
        GameEventManager.ToggleAudio?.Invoke(true);
    }

    public override void ToggleOff()
    {
        base.ToggleOff();
        GameEventManager.ToggleAudio?.Invoke(false);
    }
    //#endregion

    //#region private methods
    // private bool Load()
    // {
    //     SaveData saveData = null;
    //     Action<SaveData> callback = (a) =>
    //     {
    //         saveData = a;
    //     };

    //     GameEventManager.LoadData?.Invoke(callback);

    //     return saveData.onAudio;
    // }
    //#endregion

    //#region event handlers
    //#endregion
}
