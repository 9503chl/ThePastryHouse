using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

[Serializable]
public class SaveData : ScriptableObject
{
    public bool IsFirst;

    public int CurrentLevel = 1;
    public int ARemainCount = 20;//이름 미지정이라 일단 A
    public int RemainSnackCount;

    public float RemainPlayerHP;//남은 체력 기억.
    public List<float> ARemainHPs = new List<float>();

    public float PlayerLastPositionX;
    public float PlayerLastPositionY;

    public List<float> CirclePositionXs = new List<float>();
    public List<float> CirclePositionYs = new List<float>();
    public List<float> CircleScales = new List<float>();

    public List<float> BoxPositionXs = new List<float>();
    public List<float> BoxPositionYs = new List<float>();
    public List<float> BoxScales = new List<float>();

    public DateTime LastPlayTime;

    public MissionLevel Difficulty;


    public void DataReset()
    {
        IsFirst = true;

        CurrentLevel = 1;
        ARemainCount = 20;
        RemainSnackCount = 5;

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


        for (int i = 0; i < jsonData["CirclePositionXs"].Count; i++)
        {
            CirclePositionXs.Add(float.Parse(jsonData["CirclePositionXs"][i].ToString()));
        }
        for (int i = 0; i < jsonData["CirclePositionYs"].Count; i++)
        {
            CirclePositionYs.Add(float.Parse(jsonData["CirclePositionYs"][i].ToString()));
        }
        for (int i = 0; i < jsonData["CircleScales"].Count; i++)
        {
            CircleScales.Add(float.Parse(jsonData["CircleScales"][i].ToString()));
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
}
