//#region import
using System.Collections;
using System.Collections.Generic;
using Lofelt.NiceVibrations;
using UnityEngine;
//#endregion

public class ButtonPrivacyPolicy : MonoBehaviour
{
    //#region editors fields and properties
    [SerializeField] private string URL;
    //#endregion

    //#region public fields and properties
    //#endregion

    //#region private fields and properties
    //#endregion


    //#region life-cycle callbacks

    void OnEnable()
    {

    }

    void OnDisable()
    {

    }

    //#endregion

    //#region public methods

    public void Click()
    {
        GameEventManager.PlayVibration?.Invoke(HapticPatterns.PresetType.Selection);
        Application.OpenURL(URL);
    }

    //#endregion

    //#region private methods
    //#endregion

    //#region event handlers
    //#endregion
}
