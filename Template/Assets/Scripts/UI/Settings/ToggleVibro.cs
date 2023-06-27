//#region import
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
//#endregion

public class ToggleVibro : ToggleUI
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
                bool isOn = Load().onVibro;
                if (isOn)
                {
                    on.SetActive(true);
                    off.SetActive(false);
                }
                else
                {
                    on.SetActive(false);
                    off.SetActive(true);
                }
                this.isOn = Load().onVibro;
            });
    }

    //#endregion

    //#region public methods

    public override void ToggleOn()
    {
        base.ToggleOn();
        GameEventManager.ToggleVibration?.Invoke(true);
    }

    public override void ToggleOff()
    {
        base.ToggleOff();
        GameEventManager.ToggleVibration?.Invoke(false);
    }
    //#endregion

    //#region private methods
    //#endregion

    //#region event handlers
    //#endregion
}
