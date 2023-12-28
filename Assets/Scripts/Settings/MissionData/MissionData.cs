using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MissionLevel
{
    Easy,
    Normal,
    Hard
}
[CreateAssetMenu]
public class MissionData : ScriptableObject
{
    public MissionLevel Level;

    public float SiteRange;//시야 범위
    public float PlayerMaxHP;
    public float EnemyMaxHP;
    public float HungerPoint;
    public float PlayerSpeed;
    public float EnemySpeed;
    public float EnemyFollowTime;

    public int SnackCount;//HP 포션 갯수
    public int SnackValue;//HP 포션 회복 값
}
