using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creature
{
    private MissionData missionData;
    public override void OnStart()
    {
        base.OnStart();
        missionData = GameSetting.Instance.CurrentMissionData;

        HP = missionData.PlayerMaxHP;
        Speed = missionData.PlayerSpeed;
    }
}
