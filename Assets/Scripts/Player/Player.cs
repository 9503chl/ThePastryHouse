﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player : Creature
{
    public Image DamageImage;

    private MissionData missionData;

    private Enemy enemyProp;

    private Unit unitProp;

    private bool isDamaged = true;


    public override void OnAwake()
    {
        base.OnAwake();
        missionData = GameSetting.Instance.CurrentMissionData;
        m_Sprite = GetComponent<SpriteRenderer>();

        HP = missionData.PlayerMaxHP;
        Speed = missionData.PlayerSpeed;
        CurrentHP = HP;
        Damage = 1;
    }
    public override void EnableOn()
    {
        base.EnableOn();
        DamageImage.color = new Color(DamageImage.color.r, DamageImage.color.g, DamageImage.color.b, 0);
    }
    public override void TriggerEnterOn(Collider collider)
    {
        base.TriggerEnterOn(collider);

        Debug.Log("Player is Detected by Enemy");

        enemyProp = collider.gameObject.GetComponent<Enemy>();
        unitProp = collider.gameObject.GetComponent<Unit>();

        if (enemyProp != null)
        {
            enemyProp.StopMove();
        }
        if(unitProp != null)
        {
            unitProp.target = transform.position;
        }
    }
    public void DamageCount(float damage)
    {
        if(isDamaged == true) 
            StartCoroutine(DamageDelay(damage));
    }
    IEnumerator DamageDelay(float damage)
    {
        isDamaged = false;
        CurrentHP -= damage;
        m_Sprite.color = new Color(m_Sprite.color.r, m_Sprite.color.g, m_Sprite.color.b, 0.5f);
        DamageImage.DOColor(new Color(DamageImage.color.r, DamageImage.color.g, DamageImage.color.b, 0.4f), 0.125f);
        DamageImage.DOColor(new Color(DamageImage.color.r, DamageImage.color.g, DamageImage.color.b, 0), 0.125f);
        yield return new WaitForSeconds(0.25f);

        m_Sprite.color = new Color(m_Sprite.color.r, m_Sprite.color.g, m_Sprite.color.b, 1);
        isDamaged = true;
    }
}
