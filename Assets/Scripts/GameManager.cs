using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif 

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector]
    public string playerName;
    [HideInInspector]
    public string playerHighScore = "0";
    [HideInInspector]
    public int playerHiScore = 0;
    [HideInInspector]
    public int currentScore;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.playerName;
            playerHiScore = data.playerHiScore;
            string str = $"{playerHiScore}";
            char pad = '0';
            playerHighScore = str.PadLeft(5, pad);
        }
    }
    public void SaveGame()
    {
        if (currentScore <= playerHiScore) { return; }
        playerHiScore = currentScore;
        string str = $"{playerHiScore}";
        char pad = '0';
        playerHighScore = str.PadLeft(5, pad);
        
        SaveData data = new()
        {
            playerName = playerName,
            playerHiScore = playerHiScore
        };

        string json = JsonUtility.ToJson(data);
        
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}

[System.Serializable]
class SaveData
{
    public string playerName;
    public int playerHiScore;
}
