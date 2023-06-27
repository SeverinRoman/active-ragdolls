using System.IO;
using UnityEngine;

public static class JsonSaver
{
    public static void Save<T>(T item, string fileName)
    {
        var json = JsonUtility.ToJson(item, true);
        // Debug.Log($"@@@ Saving to {GetSavePath(fileName)}:\n{json}");
        File.WriteAllText(GetSavePath(fileName), json);
    }

    public static T Load<T>(string fileName)
    {
        var path = GetSavePath(fileName);
        if (File.Exists(path))
        {
            var json = File.ReadAllText(GetSavePath(fileName));
            // Debug.Log($"!!! Loaded from {path}:\n{json}");
            return JsonUtility.FromJson<T>(json);
        }
        else
        {
            return default;
        }
    }

    public static string GetSavePath(string fileName)
    {
        return Path.Combine(GetSaveDirectory(), fileName + ".json");
    }

    public static string GetSaveDirectory()
    {
#if UNITY_IOS
        return Application.persistentDataPath;
#else
        return Application.dataPath + "/Saves";
#endif
    }

    public static bool IsExistsSave(string fileName)
    {
        return File.Exists(GetSavePath(fileName));
    }

    public static void DeleteSave(string fileName)
    {
        if (IsExistsSave(fileName))
        {
            File.Delete(GetSavePath(fileName));
        }
    }
}