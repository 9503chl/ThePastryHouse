using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Player : Creature
{
    public Image DamageImage;

    private float DamageInterval;

    private MissionData missionData;

    private Enemy enemyProp;

    private bool isDamaged = true;


    public override void OnAwake()
    {
        base.OnAwake();
        missionData = GameSetting.Instance.CurrentMissionData;
        m_Sprite = GetComponent<SpriteRenderer>();
    }
    public override void OnStart()
    {
        base.OnStart();
        HP = missionData.PlayerMaxHP;
        Speed = missionData.PlayerSpeed;
        CurrentHP = HP;
        DamageInterval = missionData.DamageInterval;
        Damage = 1;
    }
    public override void EnableOn()
    {
        base.EnableOn();
        DamageImage.color = new Color(DamageImage.color.r, DamageImage.color.g, DamageImage.color.b, 0);
    }
    public void DamageCount(float damage)
    {
        if(isDamaged == true) 
            StartCoroutine(DamageDelay(damage));
    }
    public override void CollisionStayOn(Collision2D collision)
    {
        base.CollisionStayOn(collision);
        if(collision.gameObject.layer != 7)//적이 맞나?
        {
            if (isDamaged)
            {
                enemyProp = collision.gameObject.GetComponent<Enemy>();
                DamageCount(enemyProp.Damage);
            }
        }
    }
    IEnumerator DamageDelay(float damage)
    {
        isDamaged = false;
        CurrentHP -= damage;
        m_Sprite.color = new Color(m_Sprite.color.r, m_Sprite.color.g, m_Sprite.color.b, 0.5f);
        DamageImage.DOColor(new Color(DamageImage.color.r, DamageImage.color.g, DamageImage.color.b, 0.4f), 0.125f);
        yield return new WaitForSeconds(0.125f);
        DamageImage.DOColor(new Color(DamageImage.color.r, DamageImage.color.g, DamageImage.color.b, 0), 0.125f);
        yield return new WaitForSeconds(0.125f + DamageInterval - 0.25f);

        m_Sprite.color = new Color(m_Sprite.color.r, m_Sprite.color.g, m_Sprite.color.b, 1);
        isDamaged = true;
    }
}
