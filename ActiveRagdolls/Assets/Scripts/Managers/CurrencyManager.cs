//#region import
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#endregion

[Serializable]
public class CurrencyConfig
{
    public CurrencyType currencyType = CurrencyType.None;
    public float value = 0f;
}

public class CurrencyManager : SaveLoadObject
{
    //#region editors fields and properties
    [SerializeField] private List<CurrencyConfig> currencyConfigs = new List<CurrencyConfig>();

    //#endregion

    //#region public fields and properties
    //#endregion

    //#region private fields and properties
    //#endregion


    //#region life-cycle callbacks
    void Start()
    {
        Init();
    }

    void OnEnable()
    {
        GameEventManager.GetCurrentCurrency.AddListener(OnGetCurrentCurrency);
        GameEventManager.AddCurrency.AddListener(OnAddCurrency);
        GameEventManager.TryBuy.AddListener(OnTryBuy);
    }

    void OnDisable()
    {
        GameEventManager.GetCurrentCurrency.RemoveListener(OnGetCurrentCurrency);
        GameEventManager.AddCurrency.RemoveListener(OnAddCurrency);
        GameEventManager.TryBuy.RemoveListener(OnTryBuy);
    }

    //#endregion

    //#region public methods
    //#endregion

    //#region private methods

    private void Init()
    {
        SaveData saveData = Load();

        for (int i = 0; i < saveData.currencies.Count; i++)
        {
            for (int j = 0; j < currencyConfigs.Count; j++)
            {
                if (currencyConfigs[j].currencyType == saveData.currencies[i].currencyType)
                {
                    currencyConfigs[j].value = saveData.currencies[i].value;

                    GameEventManager.CurrencyUpdate?.Invoke(currencyConfigs[j].currencyType, currencyConfigs[j].value);
                }
            }
        }
    }

    private float GetCurrentCurrency(CurrencyType currencyType)
    {
        return currencyConfigs.Find(i => i.currencyType == currencyType).value;
    }

    private void ChangeCurrentCurrency(CurrencyType currencyType, float value)
    {
        CurrencyConfig currencyConfig = currencyConfigs.Find(i => i.currencyType == currencyType);
        currencyConfig.value += value;


        SaveData saveData = Load();
        CurrencyConfig saveCurrencyConfig = saveData.currencies.Find(i => i.currencyType == currencyType);
        saveCurrencyConfig.value = currencyConfig.value;
        Save(saveData);

        GameEventManager.CurrencyUpdate?.Invoke(currencyType, currencyConfig.value);
    }

    private bool TryBuy(CurrencyType currencyType, float value)
    {
        CurrencyConfig currencyConfig = currencyConfigs.Find(i => i.currencyType == currencyType);

        if (currencyConfig.value < value)
        {
            return false;
        }
        else
        {
            ChangeCurrentCurrency(currencyType, -value);
            return true;
        }
    }

    //#endregion

    //#region event handlers

    protected void OnGetCurrentCurrency(CurrencyType currencyType, Action<float> callback)
    {
        callback(GetCurrentCurrency(currencyType));
    }

    protected void OnAddCurrency(CurrencyType currencyType, float value)
    {
        ChangeCurrentCurrency(currencyType, value);
    }
    protected void OnTryBuy(CurrencyType currencyType, float value, Action<bool> callback)
    {
        callback(TryBuy(currencyType, value));
    }

    //#endregion
}
