using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Scripting;
using UnityEngine;
public class EnemyManager : Manager
{
    public static EnemyManager Instance;

    private Enemy enemyProp;

    public List<GameObject> EnemyList = new List<GameObject>();

    public override void OnAwake()
    {
        base.OnAwake();
        Instance = this;
        m_PoolA = PoolManager.Instance.EnemyPool;
    }

    public override void CreateProps(Transform targetTransform)
    {
        for (int i = 0; i < GameSetting.Instance.CurrentSaveData.ARemainCount; i++)
        {
            ObjProp = m_PoolA.Get();
            ObjProp.transform.name = i.ToString();
            ObjProp.transform.SetParent(targetTransform);
            ObjProp.transform.localPosition = new Vector2(Random.Range(XValue.x, XValue.y) * 10, Random.Range(YValue.x, YValue.y) * 10);
            ObjProp.transform.localRotation = Quaternion.Euler(0, 0, 0);

            EnemyList.Add(ObjProp);
        }
        ComponentOff();
    }

    public override void ResetProp()
    {
        EnemyList.RemoveRange(0, EnemyList.Count);
    }
    public override void ComponentOff()
    {
        for (int i = 0; i < EnemyList.Count; i++)
        {
            EnemyList[i].GetComponent<Enemy>().enabled = false;
            ColliderProp = ObjProp.GetComponent<Collider2D>();
            if (ColliderProp != null)
            {
                ColliderProp.enabled = false;
            }
        }
    }

    public override void ComponentOn()
    {
        for (int i = 0; i < EnemyList.Count; i++)
        {
            enemyProp = EnemyList[i].GetComponent<Enemy>();
            enemyProp.enabled = true;
            ColliderProp = ObjProp.GetComponent<Collider2D>();
            if (ColliderProp != null)
            {
                ColliderProp.enabled = true;
            }
        }
    }
}
