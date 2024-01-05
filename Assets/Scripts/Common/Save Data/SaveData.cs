using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

[Serializable]
public class SaveData : ScriptableObject
{
    public bool IsFirst;

    public int CurrentLevel;
    public int ARemainCount;//이름 미지정이라 일단 A
    public int RemainSnackCount;

    public float RemainPlayerHP;//남은 체력 기억.
    public List<float> ARemainHPs = new List<float>();

    public float PlayerLastPositionX;
    public float PlayerLastPositionY;

    public List<float> SnackPositionsXs = new List<float>();
    public List<float> SnackPositionsYs = new List<float>();

    public List<float> CylinderPositionXs = new List<float>();
    public List<float> CylinderPositionYs = new List<float>();
    public List<float> CylinderScales = new List<float>();

    public List<float> BoxPositionXs = new List<float>();
    public List<float> BoxPositionYs = new List<float>();
    public List<float> BoxScales = new List<float>();

    public DateTime LastPlayTime;

    public MissionLevel Difficulty;


    public void DataReset()
    {
        IsFirst = true;

        CurrentLevel = 0;
        ARemainCount = 0;
        RemainSnackCount = 0;

        RemainPlayerHP = 0;

        PlayerLastPositionX = 0;
        PlayerLastPositionY = 0;
    }
    public void LoadFromJson(JsonData jsonData)//Vector가 안되서 따로 나눠야함.
    {
        IsFirst = bool.Parse(jsonData["IsFirst"].ToString());

        CurrentLevel = int.Parse(jsonData["CurrentLevel"].ToString());
        ARemainCount = int.Parse(jsonData["ARemainCount"].ToString());
        RemainSnackCount = int.Parse(jsonData["RemainSnackCount"].ToString());

        RemainPlayerHP = float.Parse(jsonData["RemainPlayerHP"].ToString());

        for (int i = 0; i < jsonData["ARemainHPs"].Count; i++)
        {
            ARemainHPs.Add(float.Parse(jsonData["ARemainHPs"][i].ToString()));
        }

        PlayerLastPositionX = float.Parse(jsonData["PlayerLastPositionX"].ToString());
        PlayerLastPositionY = float.Parse(jsonData["PlayerLastPositionY"].ToString());

        for (int i = 0; i < jsonData["SnackPositionsXs"].Count; i++)
        {
            SnackPositionsXs.Add(float.Parse(jsonData["SnackPositionsXs"][i].ToString()));
        }
        for (int i = 0; i < jsonData["SnackPositionsYs"].Count; i++)
        {
            SnackPositionsYs.Add(float.Parse(jsonData["SnackPositionsYs"][i].ToString()));
        }

        for (int i = 0; i < jsonData["CylinderPositionXs"].Count; i++)
        {
            CylinderPositionXs.Add(float.Parse(jsonData["CylinderPositionXs"][i].ToString()));
        }
        for (int i = 0; i < jsonData["CylinderPositionYs"].Count; i++)
        {
            CylinderPositionYs.Add(float.Parse(jsonData["CylinderPositionYs"][i].ToString()));
        }
        for (int i = 0; i < jsonData["CylinderScales"].Count; i++)
        {
            CylinderScales.Add(float.Parse(jsonData["CylinderScales"][i].ToString()));
        }


        for (int i = 0; i < jsonData["BoxPositionXs"].Count; i++)
        {
            BoxPositionXs.Add(float.Parse(jsonData["BoxPositionXs"][i].ToString()));
        }
        for (int i = 0; i < jsonData["BoxPositionYs"].Count; i++)
        {
            BoxPositionYs.Add(float.Parse(jsonData["BoxPositionYs"][i].ToString()));
        }
        for (int i = 0; i < jsonData["BoxScales"].Count; i++)
        {
            BoxScales.Add(float.Parse(jsonData["BoxScales"][i].ToString()));
        }
        LastPlayTime = DateTime.Parse(jsonData["LastPlayTime"].ToString());
        Difficulty = (MissionLevel)int.Parse(jsonData["Difficulty"].ToString());
    }
    public void SaveToJson(GameData gameData)
    {
       IsFirst = false;

        CurrentLevel = gameData.CurrentLevel;
        ARemainCount = gameData.ARemainCount;
        RemainSnackCount = gameData.RemainSnackCount;

        RemainPlayerHP = gameData.RemainPlayerHP;
        ARemainHPs = gameData.ARemainHPs;

        PlayerLastPositionX = gameData.PlayerLastPositionX;
        PlayerLastPositionY = gameData.PlayerLastPositionY;

        SnackPositionsXs = gameData.SnackPositionsXs;
        SnackPositionsYs = gameData.SnackPositionsYs;

        CylinderPositionXs = gameData.CylinderPositionXs;
        CylinderPositionYs = gameData.CylinderPositionYs;
        CylinderScales = gameData.CylinderScales;

        BoxPositionXs = gameData.BoxPositionXs;
        BoxPositionYs = gameData.BoxPositionYs;
        BoxScales = gameData.BoxScales;

        LastPlayTime = DateTime.Now;
    }
}
