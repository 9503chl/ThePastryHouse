using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using LitJson;


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

    private List<GameObject> listProp;

    private List<float> floatList = new List<float>();
    private List<float> floatList1 = new List<float>();
    private List<float> floatList2 = new List<float>();


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
        GammaPanel.color = new Color(GammaPanel.color.r, GammaPanel.color.g, GammaPanel.color.b, 0.5f - options.Gamma / 2);
        options.Gamma = value;
    }
    public void SaveGameData()//여기다 다 적용하고 호출하기.
    {
        #region 적
        listProp = EnemyManager.Instance.EnemyList;
        CurrentSaveData.ARemainCount = listProp.Count;

        Enemy enemyProp;
        floatList.Clear();

        for (int i = 0; i < listProp.Count; i++)
        {
            enemyProp = listProp[i].GetComponent<Enemy>();
            floatList.Add(enemyProp.CurrentHP);
        }
        CurrentSaveData.ARemainHPs = floatList;

        #endregion

        #region 플레이어
        CurrentSaveData.RemainPlayerHP = PlayerInput.PlayerInputInstance.PlayerComponent.CurrentHP;
        CurrentSaveData.PlayerLastPositionX = PlayerInput.PlayerInputInstance.PlayerComponent.transform.position.x;
        CurrentSaveData.PlayerLastPositionY = PlayerInput.PlayerInputInstance.PlayerComponent.transform.position.y;
        #endregion

        #region 맵
        listProp = MapManager.Instance.CircleList;
        floatList.Clear();
        floatList1.Clear();
        floatList2.Clear();
        for (int i =0; i<listProp.Count; i++)
        {
            floatList.Add(listProp[i].transform.position.x);
            floatList1.Add(listProp[i].transform.position.y);
            floatList2.Add(listProp[i].transform.localScale.x);
        }
        CurrentSaveData.CirclePositionXs = floatList;
        CurrentSaveData.CirclePositionYs = floatList1;
        CurrentSaveData.CircleScales = floatList2;

        listProp = MapManager.Instance.BoxList;

        floatList.Clear();
        floatList1.Clear();
        floatList2.Clear();
        for (int i = 0; i < listProp.Count; i++)
        {
            floatList.Add(listProp[i].transform.position.x);
            floatList1.Add(listProp[i].transform.position.y);
            floatList2.Add(listProp[i].transform.localScale.x);
        }

        CurrentSaveData.BoxPositionXs = floatList;
        CurrentSaveData.BoxPositionYs= floatList1;
        CurrentSaveData.BoxScales= floatList2;
        #endregion
    }
    public void ListNPoolReset()
    {
        EnemyManager.Instance.ResetProps();
        MapManager.Instance.ResetProps();
        SnackManager.Instance.ResetProps();
        HPManager.Instance.ResetProps();
    }

    public void SaveToInstance()
    {
        if (CurrentSaveData != null)
        {
            CurrentSaveData.LastPlayTime = DateTime.Now;
            SaveToJson(Path.Combine(Application.persistentDataPath + GameOptionPath), options);
            SaveToJson(Path.Combine(Application.persistentDataPath + SaveDataPath), CurrentSaveData);
        }
    }

    private void OnApplicationQuit()
    {
        SaveGameData();
        ListNPoolReset();
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
