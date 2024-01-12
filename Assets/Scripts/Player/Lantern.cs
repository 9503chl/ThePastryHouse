using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : Creature
{
    private List <Enemy> enemyList= new List <Enemy>();//Enemy에 함수 만들어서 처리하기.
    public Color LanternColor;
    public override void TriggerEnterOn(Collider2D collider)
    {
        base.TriggerEnterOn(collider);
    }
    public override void TriggerExitOn(Collider2D collider)
    {
        base.TriggerExitOn(collider);
    }
    public override void OnStart()
    {
        base.OnStart();
        StartCoroutine(ListDamageCor());
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
