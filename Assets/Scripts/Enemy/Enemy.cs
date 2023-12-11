using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creature
{
    private MissionData missionData;
    public override void OnStart()
    {
        missionData = GameSetting.Instance.CurrentMissionData;
        base.OnStart();

        HP = missionData.EnemyMaxHP;
        Speed = missionData.EnemySpeed;
    }
}
