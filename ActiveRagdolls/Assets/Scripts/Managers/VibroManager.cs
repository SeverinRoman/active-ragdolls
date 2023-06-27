//#region import
using System;
using System.Collections;
using System.Collections.Generic;
using Lofelt.NiceVibrations;
using UnityEngine;
//#endregion

[RequireComponent(typeof(HapticReceiver))]
public class VibroManager : SaveLoadObject
{
    //#region public fields and properties
    //#endregion

    //#region private fields and properties
    private HapticReceiver hapticReceiver;
    //#endregion


    //#region life-cycle callbacks

    void Awake()
    {
        hapticReceiver = GetComponent<HapticReceiver>();
    }

    void Start()
    {
        ToggleVibration(Load().onVibro);
    }

    void OnEnable()
    {
        GameEventManager.PlayVibration.AddListener(OnPlayVibration);
        GameEventManager.ToggleVibration.AddListener(OnToggleVibration);
    }

    void OnDisable()
    {
        GameEventManager.PlayVibration.RemoveListener(OnPlayVibration);
        GameEventManager.ToggleVibration.AddListener(OnToggleVibration);
    }

    //#endregion

    //#region public methods
    //#endregion

    //#region private methods
    private void PlayVibration(HapticPatterns.PresetType pattern)
    {
        HapticPatterns.PlayPreset(pattern);
    }

    private void ToggleVibration(bool isOn)
    {
        hapticReceiver.hapticsEnabled = isOn;

        SaveData saveData = Load();
        saveData.onVibro = isOn;
        Save(saveData);
    }
    //#endregion

    //#region event handlers

    protected void OnPlayVibration(HapticPatterns.PresetType pattern)
    {
        PlayVibration(pattern);
    }

    protected void OnToggleVibration(bool isOn)
    {
        ToggleVibration(isOn);
    }

    //#endregion
}
