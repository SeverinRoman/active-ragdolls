using System;
using System.Collections;
using System.Collections.Generic;
using Lofelt.NiceVibrations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameEventManager
{
    //Helpers
    public static UnityEvent<CameraType, Action<Camera>> GetCamera = new();
    public static UnityEvent<HolderType, Action<Transform>> GetHolder = new();

    //Canvas
    public static UnityEvent<CanvasType, Action<Canvas>> GetCanvas = new();
    public static UnityEvent<Vector3, CameraType, CanvasType, Action<Vector3>> GetPositionRelativelyCanvas = new();
    public static UnityEvent<CanvasType, Vector3, Action<Vector3>> GetResolutionOffset = new();

    //InputManager
    public static UnityEvent SelectChooseEnd = new();
    public static UnityEvent<bool> ToggleInput = new();

    //InputCatcher
    public static UnityEvent<InputCatcherType, PointerEventData> InputCatcherDown = new();
    public static UnityEvent<InputCatcherType, PointerEventData> InputCatcherMove = new();
    public static UnityEvent<InputCatcherType, PointerEventData> InputCatcherUp = new();

    //AudioManager
    public static UnityEvent<bool> ToggleAudio = new();
    public static UnityEvent<SoundType> PlaySound = new();

    //VibroManager
    public static UnityEvent<HapticPatterns.PresetType> PlayVibration = new();
    public static UnityEvent<bool> ToggleVibration = new();

    //LevelManager
    public static UnityEvent<string> ChangeLevel = new();
    public static UnityEvent ChangeNextLevel = new();
    public static UnityEvent RestartLevel = new();
    public static UnityEvent<Action<int>> GetNumberLevel = new();

    //EffectManager
    public static UnityEvent<EffectType, Vector3, Action<GameObject>> SpawnEffect = new();
    public static UnityEvent<AnnouncerType, Vector3, Action<GameObject>, bool> SpawnAnnouncer = new();


    //UIManager
    public static UnityEvent<UIScreenType, bool> ToggleScreen = new();
    public static UnityEvent<UIScreenType, GameObject> TransferToUiScreen = new();
    public static UnityEvent<GameObject, Transform> TransferToWorld = new();

    //SaveLoadManager
    public static UnityEvent LoadDataSaveComplete = new();
    public static UnityEvent<SaveData> SaveData = new();
    public static UnityEvent<Action<SaveData>> LoadData = new();

    //СurrencyManager
    public static UnityEvent<CurrencyType, Action<float>> GetCurrentCurrency = new();
    public static UnityEvent<CurrencyType, float> AddCurrency = new();
    public static UnityEvent<CurrencyType, float, Action<bool>> TryBuy = new();
    public static UnityEvent<CurrencyType, float> CurrencyUpdate = new();

}
