using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataController : MonoBehaviour
{
    [SerializeField]
    private SunController sunController;

    [SerializeField]
    private ActionController actionController;

    [SerializeField]
    private TimerController timerController;

    [SerializeField]
    private ControlGuide controlGuide;

    [SerializeField]
    private ConstructUI constructUI;

    [SerializeField]
    private GameObject player;

    static GameObject dataContainer;
    static GameObject dContainer
    {
        get
        {
            return dataContainer;
        }
    }
    static DataController _instance;

    private void Awake()
    {
        sunController = FindObjectOfType<SunController>();
        timerController = FindObjectOfType<TimerController>();
    }

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
    public void LoadGameData()  //지정된 파일경로로부터 데이터를 불러온 후 해당데이터로 씬을 갱신한다.
    {
        string filePath = Application.persistentDataPath + GameDataFileName;
        if (File.Exists(filePath))
        {
            Debug.Log("Load");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);

            playerDataLoad();
            sunController.SunControllerSetting();
            actionController.LoadGame();
            ControlGuideLoad();
            ConstructLoad();
        }
        else
        {
            Debug.Log("New File Created");
            _gameData = new GameData();
        }
    }
    public void SaveGameData()  //지정된 파일경로로 현재 씬의 데이터를 저장한다.
    {
        playerDataSave();
        actionController.SaveGame();
        ControlGuideSave();
        ConstructSave();

        string ToJsonData = JsonUtility.ToJson(Gamedata);
        string filePath = Application.persistentDataPath + GameDataFileName;
        File.WriteAllText(filePath, ToJsonData);
        Debug.Log("Save");
    }

    public void playerDataSave()
    {
        Gamedata.playerPosition = player.transform.position;
        Gamedata.playerRotation = player.transform.rotation;

        Gamedata.playerHP = timerController.GetCurrentTime(0);
        Gamedata.playerTP = timerController.GetCurrentTime(1);
    }

    public void playerDataLoad()
    {
        player.transform.position = Gamedata.playerPosition;
        player.transform.rotation = Gamedata.playerRotation;

        timerController.SetCurrentTime(0, Gamedata.playerHP);
        timerController.SetCurrentTime(1, Gamedata.playerTP);
    }

    public void ControlGuideSave()
    {
        Gamedata.controlGuideBoolean = controlGuide.guideBoolean;
    }

    public void ControlGuideLoad()
    {
        controlGuide.guideBoolean = Gamedata.controlGuideBoolean;
        if(Gamedata.controlGuideBoolean[7] == 1)
        {
            controlGuide.sail.SetActive(false);
        }
    }

    public void ConstructSave()
    {
        Gamedata.CObjects = constructUI.Constructions;
    }

    public void ConstructLoad()
    {
        constructUI.Constructions = Gamedata.CObjects;
    }
}
