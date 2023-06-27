//#region import
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#endregion


[RequireComponent(typeof(AudioSource))]
public class AudioManager : SaveLoadObject
{
    [Serializable]
    public class SoundClipConfig
    {
        public SoundType soundType;
        public AudioClip audioClip;
    }

    //#region editors fields and properties

    [SerializeField] private List<SoundClipConfig> audioClipConfigs = new List<SoundClipConfig>();

    //#endregion

    //#region public fields and properties
    //#endregion

    //#region private fields and properties

    private AudioSource audioSource;

    //#endregion


    //#region life-cycle callbacks

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        ToggleAudio(Load().onAudio);
    }

    void OnEnable()
    {
        GameEventManager.ToggleAudio.AddListener(OnToggleAudio);
        GameEventManager.PlaySound.AddListener(OnPlaySound);
    }

    void OnDisable()
    {
        GameEventManager.ToggleAudio.RemoveListener(OnToggleAudio);
        GameEventManager.PlaySound.RemoveListener(OnPlaySound);
    }

    //#endregion

    //#region public methods
    //#endregion

    //#region private methods

    private void ToggleAudio(bool isOn)
    {
        if (isOn)
        {
            AudioListener.volume = 1f;
        }
        else
        {
            AudioListener.volume = 0f;
        }
    }

    protected void PlaySound(SoundType soundType)
    {
        foreach (SoundClipConfig audioClipConfig in audioClipConfigs)
        {
            if (audioClipConfig.soundType == soundType)
            {
                audioSource.clip = audioClipConfig.audioClip;
                audioSource.Play();
            }
        }
    }

    //#endregion

    //#region event handlers
    protected void OnToggleAudio(bool isOn)
    {
        ToggleAudio(isOn);

        SaveData saveData = Load();
        saveData.onAudio = isOn;
        Save(saveData);
    }

    protected void OnPlaySound(SoundType soundType)
    {
        PlaySound(soundType);
    }
    //#endregion
}
