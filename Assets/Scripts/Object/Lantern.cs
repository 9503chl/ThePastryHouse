using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lantern : Object
{
    private List <Enemy> enemyList= new List <Enemy>();//Enemy에 함수 만들어서 처리하기.

    public Color LanternColor;

    private Light2D light2D;

    public float Radius;
    private float angle;
    private float damageInterval;
    public float Damage;

    public int Angle;

    private CircleCollider2D circleCollider2D;

    private Vector2 target, mouse;

    private Enemy enemyProp;

    public override void TriggerEnterOn(Collider2D collider)
    {
        base.TriggerEnterOn(collider);
        circleCollider2D = collider as CircleCollider2D;
        if(circleCollider2D != null && circleCollider2D.radius != 10)
        {
            enemyProp = collider.GetComponent<Enemy>();
            if (enemyProp != null)
                enemyList.Add(enemyProp);
        }
    }
    public override void TriggerExitOn(Collider2D collider)
    {
        base.TriggerExitOn(collider);
        circleCollider2D = collider as CircleCollider2D;
        if (circleCollider2D != null && circleCollider2D.radius != 10)
        {
            enemyProp = collider.GetComponent<Enemy>();
            if (enemyProp != null)
                enemyList.Remove(enemyProp);
        }
    }
    public override void OnStart()
    {
        base.OnStart();
        damageInterval = GameSetting.Instance.CurrentMissionData.DamageInterval;
        StartCoroutine(ListDamageCor());
    }
    public override void OnAwake()
    {
        base.OnAwake();

        light2D = GetComponentInChildren<Light2D>();
        light2D.color = LanternColor;
        light2D.pointLightOuterRadius = Radius;
        light2D.pointLightInnerAngle = Angle;
        light2D.pointLightOuterAngle = Angle;
    }
    public override void OnUpdate()
    {
        base.OnUpdate();

        target = transform.position;

        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition); //유의 사항 Orthgraphic 써야함.
        angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
    IEnumerator ListDamageCor()
    {
        while (isActiveAndEnabled)
        {
            for(int i = 0;i < enemyList.Count; i++)
            {
                enemyList[i].DamageCount(Damage, damageInterval);
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
