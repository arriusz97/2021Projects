using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataController : MonoBehaviour
{
    [SerializeField]
    private SunController sunController;

    static GameObject dataContainer;
    static GameObject dContainer
    {
        get
        {
            return dataContainer;
        }
    }
    static DataController _instance;
    public static DataController Instance
    {
        get
        {
            if (!_instance)
            {
                dataContainer = new GameObject();
                dataContainer.name = "DataController";
                _instance = dataContainer.AddComponent(typeof(DataController)) as DataController;
                DontDestroyOnLoad(dataContainer);
            }
            return _instance;
        }
    }
    public string GameDataFileName = "savedata.json";
    public GameData _gameData;
    public GameData Gamedata
    {
        get
        {
            if (_gameData == null)
            {
                LoadGameData();
                SaveGameData();
            }
            return _gameData;
        }
    }
    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + GameDataFileName;
        if (File.Exists(filePath))
        {
            Debug.Log("Load");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);
            sunController.SunControllerSetting(Gamedata.currentDay);
        }
        else
        {
            Debug.Log("New File Created");
            _gameData = new GameData();
        }
    }
    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(Gamedata);
        string filePath = Application.persistentDataPath + GameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("Save");
    }
}
