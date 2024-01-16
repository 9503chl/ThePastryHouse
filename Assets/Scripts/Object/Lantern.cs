using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lantern : Object
{
    private List <Enemy> enemyList= new List <Enemy>();//Enemy에 함수 만들어서 처리하기.

    public Color LanternColor;

    private Light2D light2D;

    public float Radius;

    public int Angle;

    private Vector2 target,mouse;
    private float angleForCamera;

    private Enemy enemyProp;
    #region 부채꼴 만들기
    //private Vector2 baseVector2 = Vector3.up;

    //public List<Vector2> Vector2List;
    //private void CreateCollider()
    //{
    //    baseVector2 *= Radius;

    //    Vector2List.Add(Vector2.zero);

    //    for (int i = -Angle / 2; i <= Angle / 2; i++)
    //    {
    //        Vector2List.Add(Quaternion.AngleAxis(i, Vector3.forward) * baseVector2);
    //    }
    //    Vector2List.Add(Vector2.zero);
    //}
    #endregion
    public override void TriggerEnterOn(Collider2D collider)
    {
        base.TriggerEnterOn(collider);
        enemyProp = collider.GetComponent<Enemy>();
        if(enemyProp != null) 
            enemyList.Add(enemyProp);
    }
    public override void TriggerExitOn(Collider2D collider)
    {
        base.TriggerExitOn(collider);
        enemyProp = collider.GetComponent<Enemy>();
        if (enemyProp != null)
            enemyList.Remove(enemyProp);
    }
    public override void OnStart()
    {
        base.OnStart();
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

        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angleForCamera = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angleForCamera - 90, Vector3.forward);
    }
    IEnumerator ListDamageCor()
    {
        while(isActiveAndEnabled)
        {
            for(int i = 0;i < enemyList.Count; i++)
            {
                
            }

            yield return new WaitForEndOfFrame();
        }
    }
}
