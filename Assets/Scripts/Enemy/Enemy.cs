using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : Creature
{
    private MissionData missionData;

    private Coroutine trackingCor, searchingCor;

    private Transform searchingTr;

    private Player playerProp;

    private Unit unitProp;

    private int randomInt;

    private float MaxX = 19.2f;
    private float MaxY = 10.8f;
    private float MinX = -19.2f;
    private float MinY = -10.8f;
    public override void OnStart()
    {
        base.OnStart();

        missionData = GameSetting.Instance.CurrentMissionData;
        HP = missionData.EnemyMaxHP;
        Speed = missionData.EnemySpeed;
        Damage = 1;//나중에 바꿔야함.
    }
    public override void OnAwake()
    {
        base.OnAwake();

        unitProp = GetComponent<Unit>();
        searchingTr = transform.GetChild(0);
    }
    public override void EnableOn()
    {
        base.EnableOn();
        FullHP();
    }
    public void FullHP()
    {
        CurrentHP = HP;
    }
    private IEnumerator Searching()
    {
        unitProp.target = searchingTr;
        while (isActiveAndEnabled)
        {
            randomInt = Random.Range(0, 1);
            if(randomInt == 0)
            {
               // if(searchingTr.transform.position.x - 50 < MaxX)
            }
            else
            {

            }
            yield return new WaitForSeconds(2);
        }
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
    public override void TriggerEnterOn(Collider collider)//Stay로 해보자.
    {
        base.TriggerEnterOn(collider);
        if (collider.transform.tag == "Player")
        {
            if(searchingCor != null) StopCoroutine(searchingCor);
            trackingCor = StartCoroutine(DelayTracking(collider.gameObject));
        }
    }
    private IEnumerator DelayTracking(GameObject playerObj)
    {
        gameObject.transform.LookAt(playerObj.transform.position);//한번 바라봄
        while(isActiveAndEnabled)
        {
            yield return new WaitForSeconds(0.3f);
            unitProp.target = playerObj.transform;
            Debug.Log(playerObj.transform.position);
        }
    }
    public override void TriggerExitOn(Collider collider)
    {
        base.TriggerExitOn(collider);
        StartCoroutine(DelayStopTrack());
    }
    private IEnumerator DelayStopTrack()
    {
        yield return new WaitForSeconds(3);
        if(trackingCor!= null) 
            StopCoroutine(trackingCor);

        searchingCor = StartCoroutine(Searching());
    }
}
