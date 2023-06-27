//#region import
using System.Collections;
using System.Collections.Generic;
using Lofelt.NiceVibrations;
using UnityEngine;
//#endregion

public class ToggleUI : SaveLoadObject
{
    //#region editors fields and properties
    //#endregion

    //#region public fields and properties
    [SerializeField] public bool isOn;
    [SerializeField] public GameObject on;
    [SerializeField] public GameObject off;
    //#endregion

    //#region private fields and properties

    //#endregion


    //#region life-cycle callbacks
    //#endregion

    //#region public methods

    public void Toggle()
    {
        GameEventManager.PlayVibration?.Invoke(HapticPatterns.PresetType.Selection);
        isOn = !isOn;
        if (isOn)
        {
            ToggleOn();
        }
        else
        {
            ToggleOff();
        }
    }

    public virtual void ToggleOn()
    {
        on.SetActive(true);
        off.SetActive(false);
        isOn = true;
    }

    public virtual void ToggleOff()
    {
        on.SetActive(false);
        off.SetActive(true);
        isOn = false;
    }

    //#endregion

    //#region private methods



    //#endregion

    //#region event handlers
    //#endregion
}
