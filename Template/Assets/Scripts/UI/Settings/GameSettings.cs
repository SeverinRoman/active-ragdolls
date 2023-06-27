//#region import
using System.Collections;
using System.Collections.Generic;
using Lofelt.NiceVibrations;
using UnityEngine;
using UnityEngine.UI;
//#endregion

public class GameSettings : MonoBehaviour
{
    //#region editors fields and properties
    [SerializeField] private GameObject holderSettings;
    [SerializeField] private Image buttonSettings;

    [SerializeField] private Sprite iconButtonOpenSettings;
    [SerializeField] private Sprite iconButtonCloseSettings;
    //#endregion

    //#region public fields and properties
    //#endregion

    //#region private fields and properties

    private bool onSettings = false;

    //#endregion


    //#region life-cycle callbacks

    private void Awake()
    {
        holderSettings.SetActive(false);
    }

    //#endregion

    //#region public methods

    public void OnClickSettings()
    {
        GameEventManager.PlayVibration?.Invoke(HapticPatterns.PresetType.Selection);
        onSettings = !onSettings;
        if (onSettings)
        {
            ChangeIconButton(iconButtonCloseSettings);
            holderSettings.SetActive(true);
            PauseGame();
        }
        else
        {
            ChangeIconButton(iconButtonOpenSettings);
            holderSettings.SetActive(false);
            ResumeGame();
        }
    }

    //#endregion

    //#region private methods

    private void ChangeIconButton(Sprite sprite)
    {
        buttonSettings.sprite = sprite;
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }
    private void ResumeGame()
    {
        Time.timeScale = 1;
    }
    //#endregion

    //#region event handlers
    //#endregion
}
