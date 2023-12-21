using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public struct GameData
{
    public int CurrentLevel;
    public int ARemainCount;//이름 미지정이라 일단 A
    public int RemainSnackCount;

    public float RemainPlayerHP;//남은 체력 기억.
    public float[] ARemainHPs;

    public Vector3 PlayerLastPosition;

    public Vector3[] SnackPositions;

    public Vector4[] CylinderPosAndSizes;
    public Vector4[] BoxPosAndSizes;
}


public class GameSetting : MonoBehaviour
{
    private static GameSetting instance;

    public static GameSetting Instance {
        get {
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

    void Awake()
    {
        BGMAudios = BGMAudioGroup.GetComponentsInChildren<AudioSource>();
        EffectAudios = EffectAudioGroup.GetComponentsInChildren<AudioSource>();

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
            0.5f - options.Gamma/2);
        options.Gamma = value;
    }
    public void SaveToInstance()
    {
        if (CurrentSaveData != null)
        {
            CurrentSaveData.IsFirst = false;

            CurrentSaveData.CurrentLevel = CurrentGameData.CurrentLevel;
            CurrentSaveData.ARemainCount = CurrentGameData.ARemainCount;
            CurrentSaveData.RemainSnackCount = CurrentGameData.RemainSnackCount;

            CurrentSaveData.RemainPlayerHP = CurrentGameData.RemainPlayerHP;
            CurrentSaveData.ARemainHPs = CurrentGameData.ARemainHPs;

            CurrentSaveData.PlayerLastPosition = CurrentGameData.PlayerLastPosition;
            CurrentSaveData.SnackPositions = CurrentGameData.SnackPositions;

            CurrentSaveData.CylinderPosAndSizes = CurrentGameData.CylinderPosAndSizes;
            CurrentSaveData.BoxPosAndSizes = CurrentGameData.BoxPosAndSizes;

            CurrentSaveData.LastPlayTime = DateTime.Now;
        }
        else
        {
            Debug.Log("SaveData is empty");
        }
    }
    private void OnApplicationQuit()// 강제종료시도 저장
    {
        SaveToInstance();
    }
}
