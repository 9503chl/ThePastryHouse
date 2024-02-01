using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Mono.Cecil.Cil;

public class Enemy : Creature
{
    private MissionData missionData;

    private Coroutine trackingCor, searchingCor;

    private Transform childTr;

    private AIDestinationSetter aIDestinationSetter;

    private AILerp aILerp;

    private float followTime;
    private float time;

    public override void OnStart()
    {
        base.OnStart();

        missionData = GameSetting.Instance.CurrentMissionData;
        HP = missionData.EnemyMaxHP;
        aILerp.speed = missionData.EnemySpeed;
        followTime = missionData.EnemyFollowTime;
        FullHP();

        Damage = 75;//나중에 바꿔야함.
    }
    public override void OnAwake()
    {
        base.OnAwake();
        m_Sprite = GetComponent<SpriteRenderer>();
        #region 컴포넌트 얻고 자신 컴포넌트는 마지막에 넣기.
        MonoBehaviour[] monos = GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour mono in monos)
        {
            monoList.Add(mono);
        }

        monos = GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour mono in monos)
        {
            monoList.Add(mono);
        }

        monoList.Remove(GetComponent<Enemy>());
        monoList.Add(GetComponent<Enemy>());
        #endregion
        childTr = transform.GetChild(0);
        aIDestinationSetter = GetComponent<AIDestinationSetter>();
        aILerp = GetComponent<AILerp>();
    }
    public override void EnableOn()
    {
        base.EnableOn();

        ComponentOn(monoList);

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

    public override void TriggerEnterOn(Collider2D collider)
    {
        base.TriggerEnterOn(collider);
        if (collider.transform.tag == "Player")
        {
            if (searchingCor != null) StopCoroutine(searchingCor);
            if (trackingCor == null) trackingCor = StartCoroutine(DelayTracking(collider.gameObject));
        }
    }
    public override void DamageCount(float damage, float damageInterval)
    {
        if(CurrentHP > 0)
        {
            base.DamageCount(damage, damageInterval);
        }
        else
        {
            StartCoroutine(DieCor());
        }
    }
    public override IEnumerator DieCor()
    {
        yield return base.DieCor();

        ComponentOff(monoList);

        yield return new WaitForSeconds(m_Animator.GetCurrentAnimatorClipInfo(0).Length);

        gameObject.transform.position = PoolManager.Instance.VectorAway;
        PoolManager.Instance.EnemyPool.Release(gameObject);
    }
    private IEnumerator DelayTracking(GameObject playerObj)
    {
        time = 0;
        while (time < followTime)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            time += Time.deltaTime;
            childTr.position = playerObj.transform.position;
            aILerp.SearchPath();
        }
        searchingCor = StartCoroutine(Searching());
        trackingCor = null;
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
