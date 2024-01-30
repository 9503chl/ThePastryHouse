using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    private GameObject prop;
    private Collider2D[] colliderProps;

    private Enemy enemyProp;

    public List<GameObject> EnemyList = new List<GameObject>();

    private int enemyCount;

    public float MaxX;
    public float MaxY;

    public float MinX;
    public float MinY;

    private void Awake()
    {
        Instance = this;

        enemyCount = PoolManager.Instance.EnemyPoolSize;
    }
    public void EnemyCreate(Transform targetTF)//아직 갯수 기억해서 소환하진 않음
    {
        for(int i = 0; i < enemyCount; i++)
        {
            prop = PoolManager.Instance.EnemyPool.Get();
            prop.transform.name = i.ToString();
            prop.transform.SetParent(targetTF);
            prop.transform.localPosition = new Vector3(Random.Range(MinX, MaxX) * 10, Random.Range(MinY, MaxY) * 10, 0);
            prop.transform.localRotation = Quaternion.Euler(0, 0, 0);

            EnemyList.Add(prop);
        }
        EnemyComponentOff();
    }
    public void EnemyReset()
    {
        EnemyList.RemoveRange(0, EnemyList.Count);
    }
    public void EnemyComponentOff()
    {
        for(int i= 0; i< EnemyList.Count; i++) 
        {
            EnemyList[i].GetComponent<Enemy>().enabled = false;
            colliderProps = prop.GetComponents<Collider2D>();
            if (colliderProps != null)
            {
                for (int j = 0; j < colliderProps.Length; j++)
                {
                    colliderProps[j].enabled = false;
                }
            }
        }
    }
    public void EnemyComponentOn()
    {
        for (int i = 0; i < EnemyList.Count; i++)
        {
            enemyProp = EnemyList[i].GetComponent<Enemy>();
            enemyProp.enabled = true;
            colliderProps = prop.GetComponents<Collider2D>();
            if (colliderProps != null)
            {
                for (int j = 0; j < colliderProps.Length; j++)
                {
                    colliderProps[j].enabled = true;
                }
            }
        }
    }
}
