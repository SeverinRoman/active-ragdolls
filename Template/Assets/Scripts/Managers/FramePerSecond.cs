//#region import
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//#endregion

public class FramePerSecond : MonoBehaviour
{
    //#region editors fields and properties
    [SerializeField] private bool showFps = true;
    [SerializeField] private int targetFrameRate = 60;
    [SerializeField] private bool vSync = true;
    [SerializeField] private int vSyncCount = 0;
    [SerializeField] private TMP_Text text;
    //#endregion

    //#region public fields and properties
    //#endregion

    //#region private fields and properties
    //#endregion


    //#region life-cycle callbacks
    void Start()
    {
        if (!showFps)
        {
            text.text = "";
        }

        SetVsync();
        SetTargetFrameRate();
    }

    void Update()
    {
        if (showFps)
        {
            int current = (int)(1f / Time.unscaledDeltaTime);
            text.text = "FPS: " + current.ToString();
        }
    }

    //#endregion

    //#region public methods
    //#endregion

    //#region private methods

    private void SetVsync()
    {
        if (!vSync) return;
        QualitySettings.vSyncCount = vSyncCount;
    }
    private void SetTargetFrameRate()
    {
        if (targetFrameRate == 0) return;

        Application.targetFrameRate = targetFrameRate;

    }
    //#endregion

    //#region event handlers
    //#endregion
}
