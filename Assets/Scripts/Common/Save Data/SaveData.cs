using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SaveData : ScriptableObject
{
    public bool IsFirst;

    public int CurrentLevel;
    public int ARemainCount;//�̸� �������̶� �ϴ� A
    public int RemainSnackCount;

    public float RemainPlayerHP;//���� ü�� ���.
    public float[] ARemainHPs;

    public Vector3 PlayerLastPosition;

    public Vector3[] SnackPositions; 

    public Vector4[] CylinderPosAndSizes;
    public Vector4[] BoxPosAndSizes;

    public DateTime LastPlayTime;

    public MissionLevel Difficulty;

public void DataReset()
    {
        IsFirst = true;

        CurrentLevel = 0;
        ARemainCount = 0;
        RemainSnackCount = 0;

        RemainPlayerHP = 0;

        PlayerLastPosition = Vector3.zero;
    }
}
