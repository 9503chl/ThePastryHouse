using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lantern : Object
{
    private List <Enemy> enemyList= new List <Enemy>();//Enemy에 함수 만들어서 처리하기.

    public Color LanternColor;

    public float Radius;
    public int Angle;

    private EdgeCollider2D edgeCollider;

    private Enemy enemyProp;

    //private Vector2 baseVector2 = Vector3.up;

    //public List<Vector2> Vector2List;


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

        edgeCollider = GetComponent<EdgeCollider2D>();

        //baseVector2 *= Radius;

        //Vector2List.Add(Vector2.zero);

        //for (int i = -Angle / 2; i<= Angle / 2; i++) 
        //{
        //    Vector2List.Add(Quaternion.AngleAxis(i, Vector3.forward) * baseVector2);
        //}
        //Vector2List.Add(Vector2.zero);

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
