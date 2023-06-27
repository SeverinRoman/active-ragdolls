//#region import
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#endregion

public class SaveLoadObject : MonoBehaviour
{
    //#region editors fields and properties
    //#endregion

    //#region public fields and properties
    //#endregion

    //#region private fields and properties
    //#endregion


    //#region life-cycle callbacks
    //#endregion

    //#region public methods
    public virtual SaveData Load()
    {
        SaveData saveData = null;
        Action<SaveData> callback = (a) =>
        {
            saveData = a;
        };

        GameEventManager.LoadData?.Invoke(callback);

        return saveData;
    }

    public virtual void Save(SaveData saveData)
    {
        Action<SaveData> callback = (a) =>
        {
            saveData = a;
        };
        GameEventManager.SaveData?.Invoke(saveData);
    }
    //#endregion

    //#region private methods
    //#endregion

    //#region event handlers
    //#endregion
}
