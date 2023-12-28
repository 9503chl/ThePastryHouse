using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : Creature
{
    private MissionData missionData;

    private Coroutine trackingCor, searchingCor;

    private Transform childTr;

    private AIDestinationSetter aIDestinationSetter;

    private AILerp aILerp;

    private IAstarAI aiProp;

    private float followTime;

    //private float MaxX = 1920f;
    //private float MaxZ = 1080f;
    //private float MinX = -1920f;
    //private float MinZ = -1080f;

    //private Vector2 randomVec2;

    public override void OnStart()
    {
        base.OnStart();

        missionData = GameSetting.Instance.CurrentMissionData;
        HP = missionData.EnemyMaxHP;
        aILerp.speed = missionData.EnemySpeed;
        followTime = missionData.EnemyFollowTime;
        FullHP();

        Damage = 1;//나중에 바꿔야함.
    }
    public override void OnAwake()
    {
        base.OnAwake();
        childTr = transform.GetChild(0);
        aIDestinationSetter = GetComponent<AIDestinationSetter>();
        aiProp = GetComponent<IAstarAI>();
        aILerp = GetComponent<AILerp>();
    }
    public override void EnableOn()
    {
        base.EnableOn();
        aIDestinationSetter.enabled = true;
        searchingCor = StartCoroutine(Searching());
    }
    public void FullHP()
    {
        CurrentHP = HP;
    }
    private IEnumerator Searching()
    {
        childTr.position = transform.position;
        while (isActiveAndEnabled)
        {
            yield return new WaitForSeconds(2);
        }
    }
    public override void DisableOn()
    {
        base.DisableOn();
        aIDestinationSetter.enabled = false;
    }

    public override void TriggerEnterOn(Collider2D collider)
    {
        base.TriggerEnterOn(collider);
        if (collider.transform.tag == "Player")
        {
            if(searchingCor != null) StopCoroutine(searchingCor);
            if(trackingCor == null) trackingCor = StartCoroutine(DelayTracking(collider.gameObject));
        }
    }
    private IEnumerator DelayTracking(GameObject playerObj)
    {
        while (isActiveAndEnabled)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            childTr.position = playerObj.transform.position;
            aiProp.SearchPath();
        }
    }
    public override void TriggerExitOn(Collider2D collider)
    {
        base.TriggerExitOn(collider);
        StartCoroutine(DelayStopTrack());
    }
    private IEnumerator DelayStopTrack()
    {
        yield return new WaitForSeconds(followTime);
        if (trackingCor != null)
        {
            StopCoroutine(trackingCor);
            trackingCor = null;
        }

        searchingCor = StartCoroutine(Searching());
    }
}
