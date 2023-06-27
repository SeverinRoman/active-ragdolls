//#region import
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#endregion

[Serializable]
public class SaveData
{
    public bool onAudio = true;
    public bool onVibro = true;

    public int level = 0;

    public List<CurrencyConfig> currencies = new List<CurrencyConfig>();
}