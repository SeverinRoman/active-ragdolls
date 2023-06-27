using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;
using NaughtyAttributes;

public class LevelManager : SaveLoadObject
{
    [System.Serializable]
    private class LevelConfig
    {
        [Scene]
        public string sceneName;
        [SerializeField][ReadOnly][AllowNesting] public int level;
    }
    [SerializeField] private bool isDebug = false;
    [SerializeField] private List<LevelConfig> levelConfigs = new List<LevelConfig>();

    private LevelConfig currentLevel = null;

    void Awake()
    {
        Init();
    }

    void OnEnable()
    {
        GameEventManager.ChangeLevel.AddListener(OnChangeLevel);
        GameEventManager.ChangeNextLevel.AddListener(OnChangeNextLevel);
        GameEventManager.RestartLevel.AddListener(OnRestartLevel);
        GameEventManager.GetNumberLevel.AddListener(OnGetNumberLevel);
    }

    void OnDisable()
    {
        GameEventManager.ChangeLevel.RemoveListener(OnChangeLevel);
        GameEventManager.ChangeNextLevel.RemoveListener(OnChangeNextLevel);
        GameEventManager.RestartLevel.RemoveListener(OnRestartLevel);
        GameEventManager.GetNumberLevel.RemoveListener(OnGetNumberLevel);
    }

    void Start()
    {
        Debug.Log(("level", GetNumberLevel()));
        Application.targetFrameRate = 60;
    }

    private void LoadLevel()
    {
        int empty = 0;
        DOTween.To(() => empty, x => empty = x, 1, 0f).SetLink(gameObject).OnComplete(() =>
        {
            SaveData saveData = Load();
            if (levelConfigs.Count <= saveData.level) return;
            if (currentLevel.sceneName != levelConfigs[saveData.level].sceneName)
            {
                ChangeLevel(levelConfigs[saveData.level].sceneName);
            }
            GameEventManager.ToggleScreen?.Invoke(UIScreenType.Load, false);
        });
    }

    private void SaveLevel()
    {
        int currentLevel = GetNumberLevel();

        if (currentLevel >= levelConfigs.Count)
        {
            currentLevel = 0;
        }
        SaveData saveData = Load();
        saveData.level = currentLevel;
        Save(saveData);
    }

    private void Init()
    {
#if !UNITY_EDITOR
        isDebug = false;
#endif
#if UNITY_EDITOR
        int empty = 0;
        DOTween.To(() => empty, x => empty = x, 1, 0f).SetLink(gameObject).OnComplete(() =>
        {
            GameEventManager.ToggleScreen?.Invoke(UIScreenType.Load, false);
        });
#endif
        if (isDebug) return;


        GameEventManager.ToggleScreen?.Invoke(UIScreenType.Load, true);

        foreach (LevelConfig levelConfig in levelConfigs)
        {
            if (levelConfig.sceneName == SceneManager.GetActiveScene().name)
            {
                currentLevel = levelConfig;
            }
        }

        if (currentLevel == null)
        {
            ChangeLevel(levelConfigs[0].sceneName);
            return;
        }
        else
        {
            LoadLevel();
        }

    }

    private void ChangeLevel(string sceneNextLevel)
    {
        SceneManager.LoadScene(sceneNextLevel);
    }

    private int GetNumberLevel()
    {
        int levelNumber = 0;

        for (int i = 0; i < levelConfigs.Count; i++)
        {
            levelNumber += 1;
            if (SceneManager.GetActiveScene().name == levelConfigs[i].sceneName)
            {
                break;
            }
        }
        return levelNumber;
    }
    [NaughtyAttributes.Button]
    private void UpdateIndex()
    {
        for (int i = 0; i < levelConfigs.Count; i++)
        {
            levelConfigs[i].level = i + 1;
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ChangeNextLevel()
    {
        SaveLevel();

        for (int i = 0; i < levelConfigs.Count; i++)
        {
            if (SceneManager.GetActiveScene().name == levelConfigs[i].sceneName)
            {
                if (i + 1 < levelConfigs.Count)
                {
                    ChangeLevel(levelConfigs[i + 1].sceneName);
                }
                else
                {
                    ChangeLevel(levelConfigs[0].sceneName);
                }
            }
        }
    }

    protected void OnChangeLevel(string sceneName)
    {
        ChangeLevel(sceneName);
    }

    protected void OnRestartLevel()
    {
        RestartLevel();
    }

    protected void OnChangeNextLevel()
    {
        ChangeNextLevel();
    }

    protected void OnGetNumberLevel(Action<int> callback)
    {
        callback(GetNumberLevel());
    }
}