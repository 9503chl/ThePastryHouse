﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creature
{
    private MissionData missionData;

    public override void OnAwake()
    {
        base.OnAwake();
        missionData = GameSetting.Instance.CurrentMissionData;


        HP = missionData.PlayerMaxHP;
        Speed = missionData.PlayerSpeed;
    }
    public override void CollisionEnterOn()
    {
        base.CollisionEnterOn();
        Debug.Log(transform.name + "is Enter Collision");
    }
}
