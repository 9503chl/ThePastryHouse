using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using LitJson;

public struct GameData
{
    public int CurrentLevel;
    public int ARemainCount;//이름 미지정이라 일단 A
    public int RemainSnackCount;

    public float RemainPlayerHP;//남은 체력 기억.
    public List<float> ARemainHPs;

    public float PlayerLastPositionX;
    public float PlayerLastPositionY;

    public List<float> SnackPositionsXs;
    public List<float> SnackPositionsYs;

    public List<float> CirclePositionXs;
    public List<float> CirclePositionYs;
    public List<float> CircleScales;

    public List<float> BoxPositionXs;
    public List<float> BoxPositionYs;
    public List<float> BoxScales;

    public void SetGameDate(SaveData saveData)
    {
        CurrentLevel= saveData.CurrentLevel;
        ARemainCount= saveData.ARemainCount;
        RemainSnackCount= saveData.RemainSnackCount;

        RemainPlayerHP= saveData.RemainPlayerHP;
        ARemainHPs = saveData.ARemainHPs;

        PlayerLastPositionX = saveData.PlayerLastPositionX;
        PlayerLastPositionY = saveData.PlayerLastPositionY;

        SnackPositionsXs= saveData.SnackPositionsXs;
        SnackPositionsYs= saveData.SnackPositionsYs;

        CirclePositionXs= saveData.CylinderPositionXs;
        CirclePositionYs= saveData.CylinderPositionYs;    
        CircleScales= saveData.CylinderScales;

        BoxPositionXs= saveData.BoxPositionXs;
        BoxPositionYs= saveData.BoxPositionYs;
        BoxScales= saveData.BoxScales;
    }
}


public class GameSetting : MonoBehaviour
{
    private static GameSetting instance;

    public static GameSetting Instance
    {
        get
        {
            GameSetting[] templates = FindObjectsOfType<GameSetting>();

            if (templates != null)
            {
                instance = templates[0];
                instance.enabled = true;
                instance.gameObject.SetActive(true);
            }

            return instance;
        }
    }

    public SaveData CurrentSaveData;

    public GameData CurrentGameData;//구조체로 현재 정보 갖고 있다가 저장시 옮기기.

    public MissionData CurrentMissionData;
    public MissionLevel CurrentMissionLevel;

    public Options options;

    public GameObject BGMAudioGroup;
    public GameObject EffectAudioGroup;

    public Image GammaPanel;

    private AudioSource[] BGMAudios;
    private AudioSource[] EffectAudios;

    public string GameOptionPath;
    public string SaveDataPath;

    private JsonData jsonDataOption;
    private JsonData jsonDataSaveData;

    void Awake()
    {
        BGMAudios = BGMAudioGroup.GetComponentsInChildren<AudioSource>();
        EffectAudios = EffectAudioGroup.GetComponentsInChildren<AudioSource>();

        options = ScriptableObject.CreateInstance<Options>();
        jsonDataOption = LoadFromJson(Path.Combine(Application.persistentDataPath + GameOptionPath), options);
        options.LoadFromJson(jsonDataOption);

        CurrentSaveData = ScriptableObject.CreateInstance<SaveData>();
        jsonDataSaveData = LoadFromJson(Path.Combine(Application.persistentDataPath + SaveDataPath), CurrentSaveData);
        CurrentSaveData.LoadFromJson(jsonDataSaveData);

        foreach (AudioSource a in BGMAudios)
        {
            a.volume = options.BGMVolumn;
        }
        foreach (AudioSource a in EffectAudios)
        {
            a.volume = options.EffectVolumn;
        }
        GammaPanel.color = new Color(GammaPanel.color.r, GammaPanel.color.g, GammaPanel.color.b, 0.5f - options.Gamma / 2);
    }
    public void BGMVolunmChange(float value)
    {
        foreach (AudioSource a in BGMAudios)
        {
            a.volume = value;
        }
        options.BGMVolumn = value;
    }
    public void EffectVolunmChange(float value)
    {
        foreach (AudioSource a in EffectAudios)
        {
            a.volume = value;
        }
        options.EffectVolumn = value;
    }
    public void GammaChange(float value)
    {
        GammaPanel.color = new Color(GammaPanel.color.r, GammaPanel.color.g, GammaPanel.color.b,
            0.5f - options.Gamma / 2);
        options.Gamma = value;
    }
    public void SaveGameData()//여기다 다 적용하고 호출하기.
    {
        #region 적
        List<GameObject> listProp = EnemyManager.Instance.EnemyList;
        CurrentGameData.ARemainCount = listProp.Count;

        Enemy enemyProp;
        for (int i = 0; i < listProp.Count; i++)
        {
            enemyProp = listProp[i].GetComponent<Enemy>();
            CurrentGameData.ARemainHPs.Add(enemyProp.CurrentHP);

            listProp[i].transform.position = PoolManager.Instance.VectorAway;
            PoolManager.Instance.EnemyPool.Release(listProp[i].gameObject);
        }
        #endregion

        #region 플레이어
        CurrentGameData.PlayerLastPositionX = PlayerInput.PlayerInputInstance.PlayerComponent.transform.position.x;
        CurrentGameData.PlayerLastPositionY = PlayerInput.PlayerInputInstance.PlayerComponent.transform.position.y;
        #endregion

        #region 맵
        listProp = MapManager.Instance.CircleList;
        for(int i =0; i<listProp.Count; i++)
        {
            CurrentGameData.CirclePositionXs.Add(listProp[i].transform.position.x);
            CurrentGameData.CirclePositionYs.Add(listProp[i].transform.position.y);
            CurrentGameData.CircleScales.Add(listProp[i].transform.localScale.x);
        }

        listProp = MapManager.Instance.BoxList;
        for (int i = 0; i < listProp.Count; i++)
        {
            CurrentGameData.BoxPositionXs.Add(listProp[i].transform.position.x);
            CurrentGameData.BoxPositionYs.Add(listProp[i].transform.position.y);
            CurrentGameData.BoxScales.Add(listProp[i].transform.localScale.x);
        }
        #endregion

        #region 초기화
        EnemyManager.Instance.EnemyReset();
        MapManager.Instance.BoxNCubeReset();
        #endregion
    }

    public void SaveToInstance()
    {
        if (CurrentSaveData != null)
        {
            CurrentSaveData.SaveToJson(CurrentGameData);
            SaveToJson(Path.Combine(Application.persistentDataPath + GameOptionPath), options);
        }
        else
        {
            Debug.Log("SaveData is empty");
        }
    }


    private void OnApplicationQuit()// 강제종료시도 저장
    {
        SaveGameData();
        SaveToInstance();
    }
    private void SaveToJson(string path, object obj)
    {
        string json = JsonMapper.ToJson(obj);
        File.WriteAllText(path, json);
    }
    private JsonData LoadFromJson(string path, object obj)
    {
        JsonData data = new JsonData();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonMapper.ToObject(json);
        }
        else
        {
            SaveToJson(path, obj);
        }
        return data;
    }
}
