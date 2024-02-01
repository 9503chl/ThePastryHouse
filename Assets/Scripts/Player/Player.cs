 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Player : Creature
{
    public Image DamageImage;

    public Text DamageText;

    public float damageInterval;

    private Lantern lantern;

    private MissionData missionData;

    private Enemy enemyProp;

    public Collider2D collider2DProp;

    public override void OnAwake()
    {
        base.OnAwake();

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

        monoList.Remove(GetComponent<Player>());
        monoList.Add(GetComponent<Player>());
        #endregion

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

        ComponentOn(monoList);

        DamageImage.color = new Color(DamageImage.color.r, DamageImage.color.g, DamageImage.color.b, 0);
    }
    public override void DamageCount(float damage, float damageInterval, Image damageImage)
    {
        if(CurrentHP > 0)
        {
            base.DamageCount(damage, damageInterval, damageImage);
            HPManager.Instance.HpText.text = CurrentHP.ToString();
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

        PanelManager.Instance.ActiveView = PanelManager.ViewKind.Ending;
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
