//#region import
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
//#endregion


public class SaveLoadManager : MonoBehaviour
{
    //#region editors fields and properties
    [SerializeField] private SaveData saveData = new SaveData();
    [SerializeField] private bool deleteSave = false;
    [SerializeField] private string fileName = "Save";
    //#endregion
    //#region public fields and properties
    //#endregion
    //#region private fields and properties



    //#endregion


    //#region life-cycle callbacks
    void Awake()
    {
        if (deleteSave)
        {
            DeleteSave();
        }
        Init();
    }

    void OnEnable()
    {
        GameEventManager.SaveData.AddListener(OnSaveData);
        GameEventManager.LoadData.AddListener(OnLoadData);
    }

    void OnDisable()
    {
        GameEventManager.SaveData.RemoveListener(OnSaveData);
        GameEventManager.LoadData.RemoveListener(OnLoadData);
    }

    //#endregion

    //#region public methods
    //#endregion

    //#region private methods

    private void Init()
    {
        if (!JsonSaver.IsExistsSave(fileName))
        {
            var json = new JsonValueGeneric<SaveData>(saveData);
            JsonSaver.Save(json, fileName);
        }
        else
        {
            saveData = Load();
            GameEventManager.LoadDataSaveComplete?.Invoke();
        }
    }

    private void Save(SaveData newSaveData)
    {
        var json = new JsonValueGeneric<SaveData>(newSaveData);
        JsonSaver.Save(json, fileName);
    }

    private SaveData Load()
    {
        var json = JsonSaver.Load<JsonValueGeneric<SaveData>>(fileName);
        return json.Value;
    }

    [NaughtyAttributes.Button]
    private void DeleteSave()
    {
        JsonSaver.DeleteSave(fileName);
    }

    //#endregion

    //#region event handlers

    protected void OnSaveData(SaveData newSaveData)
    {
        Save(newSaveData);
        saveData = newSaveData;
    }

    protected void OnLoadData(Action<SaveData> callback)
    {
        callback(Load());
    }
    //#endregion
}
