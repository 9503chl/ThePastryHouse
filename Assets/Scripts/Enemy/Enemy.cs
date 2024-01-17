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

    private float followTime;

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
        m_Sprite = GetComponent<SpriteRenderer>();
        childTr = transform.GetChild(0);
        aIDestinationSetter = GetComponent<AIDestinationSetter>();
        aILerp = GetComponent<AILerp>();
    }
    public override void EnableOn()
    {
        base.EnableOn();
        aIDestinationSetter.enabled = true;
        aILerp.enabled = true;
        if (searchingCor!= null)
            StopCoroutine(searchingCor);
        searchingCor = null;
        searchingCor = StartCoroutine(Searching());
    }
    public void FullHP()
    {
        CurrentHP = HP;
    }
    private IEnumerator Searching()
    {
        while (isActiveAndEnabled)
        {
            yield return new WaitForSeconds(2);
            childTr.position = transform.position;
            childTr.position += new Vector3(1, 1, 0) * Random.Range(-10, 10);
            aILerp.SearchPath();
            yield return new WaitUntil(() => aILerp.reachedEndOfPath);
        }
    }
    public override void DisableOn()
    {
        base.DisableOn();
        aIDestinationSetter.enabled = false;
        aILerp.enabled = false;
    }

    public override void TriggerEnterOn(Collider2D collider)
    {
        base.TriggerEnterOn(collider);
        if (collider.transform.tag == "Player")
        {
            if (searchingCor != null) StopCoroutine(searchingCor);
            if(trackingCor == null) trackingCor = StartCoroutine(DelayTracking(collider.gameObject));
        }
    }
    private IEnumerator DelayTracking(GameObject playerObj)
    {
        while (isActiveAndEnabled)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            childTr.position = playerObj.transform.position;
            aILerp.SearchPath();
        }
    }
    public override void TriggerExitOn(Collider2D collider)
    {
        if (collider.transform.tag == "Player")
        {
            base.TriggerExitOn(collider);
            StartCoroutine(DelayStopTrack());
        }
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
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (aIDestinationSetter.target.localPosition.x < 0)
            m_Sprite.flipX = true;
        else
            m_Sprite.flipX = false;
    }
}
