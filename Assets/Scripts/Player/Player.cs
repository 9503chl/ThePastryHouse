using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Player : Creature
{
    public Image DamageImage;

    public Text DamageText;

    private float damageInterval;

    private Lantern lantern;

    private MissionData missionData;

    private Enemy enemyProp;

    private Collider2D collider2DProp;


    public override void OnAwake()
    {
        base.OnAwake();
        m_Sprite = GetComponent<SpriteRenderer>();
        collider2DProp = GetComponent<Collider2D>();
    }
    public override void OnStart()
    {
        base.OnStart();
        missionData = GameSetting.Instance.CurrentMissionData;
        lantern = GetComponentInChildren<Lantern>();
        HP = missionData.PlayerMaxHP;
        Speed = missionData.PlayerSpeed;
        CurrentHP = HP;
        damageInterval = missionData.DamageInterval;
        Damage = 1;
        lantern.Damage = Damage;
        DamageText.text = Damage.ToString();
    }
    public override void EnableOn()
    {
        base.EnableOn();
        transform.position = Vector3.zero;
        collider2DProp.enabled = true;
        DamageImage.color = new Color(DamageImage.color.r, DamageImage.color.g, DamageImage.color.b, 0);
    }
    public override void DamageCount(float damage, float damageInterval, Image damageImage)
    {
       base.DamageCount(damage, damageInterval, damageImage);
       HPManager.Instance.HpText.text = CurrentHP.ToString();
    }
    public override void CollisionStayOn(Collision2D collision)
    {
        base.CollisionStayOn(collision);
        if(collision.gameObject.layer == 6)//적이 맞나?
        {
            if (isDamaged)
            {
                enemyProp = collision.gameObject.GetComponent<Enemy>();
                if(enemyProp != null) 
                    DamageCount(enemyProp.Damage, damageInterval, DamageImage);
            }
        }
    }
}
