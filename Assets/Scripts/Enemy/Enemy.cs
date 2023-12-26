using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Creature
{
    private MissionData missionData;

    private Coroutine coroutine;

    private Player playerProp;

    private bool isHorizon;
    public override void OnStart()
    {
        missionData = GameSetting.Instance.CurrentMissionData;
        base.OnStart();

        HP = missionData.EnemyMaxHP;
        Speed = missionData.EnemySpeed;
        Damage = 1;
    }
    public override void EnableOn()
    {
        base.EnableOn();
        int random = Random.Range(0, 1);
        isHorizon = random == 1 ? false : true;
        CurrentHP = HP;
    }

    public override void CollisionEnterOn(Collision collision)//충돌시 플레이어 따라가고
    {
        base.CollisionEnterOn(collision);
        if(collision.transform.tag == "Player")
        {
            playerProp = collision.transform.GetComponent<Player>();
            if(playerProp != null )
            {
                playerProp.DamageCount(Damage);
            }
        }
    }
    public override void TriggerEnterOn(Collider collider)//방향 구분성이 아직 없음.
    {
        base.TriggerEnterOn(collider);
        //StopMove();
        //isHorizon = false;
        //collider.enabled = false;
        //StartMove();
        Debug.Log("Enemy Trigger On");
    }
    public void StartMove()
    {
        coroutine = StartCoroutine(MoveAI());
    }
    public void StopMove()
    {
        if(coroutine != null) 
            StopCoroutine(MoveAI());
    }
    IEnumerator MoveAI()
    {
        while (isActiveAndEnabled)
        {

        }
        yield return null;
    }
}
