using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    private GameObject prop;
    private Collider[] colliderProps;

    private Enemy enemyProp;

    private List<GameObject> enemies = new List<GameObject>();

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
    public void EnemyCreate(Transform targetTF)
    {
        for(int i = 0; i < enemyCount; i++)
        {
            prop = PoolManager.Instance.EnemyPool.Get();
            prop.transform.SetParent(targetTF);
            prop.transform.localPosition = new Vector3(Random.Range(MinX, MaxX) * 100, Random.Range(MinY, MaxY) * 100, 0);

            enemyProp = prop.GetComponent<Enemy>();
            colliderProps = prop.GetComponents<Collider>();

            if (enemyProp != null)
            {
                enemyProp.enabled = true;
            }
            if(colliderProps != null)
            {
                for(int j = 0; j<colliderProps.Length; j++)//이거 되는지 확인해야됨.
                {
                     colliderProps[j].enabled = true;
                }
            }
            enemies.Add(prop);
        }
        EnemyComponentOff();
    }
    public void EnemyComponentOff()
    {
        for(int i= 0; i< enemies.Count; i++) 
        {
            enemies[i].GetComponent<Unit>().enabled = false;
            enemies[i].GetComponent<Enemy>().enabled = false;
        }
    }
    public void EnemyComponentOn()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<Unit>().enabled = true;
            enemies[i].GetComponent<Enemy>().enabled = true;
        }
    }

    public void EnemyReset()//회수
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemyProp = enemies[i].GetComponent<Enemy>();
            colliderProps = prop.GetComponents<Collider>();
            if (enemyProp != null)
            {
                enemyProp.enabled = false;
            }
            if (colliderProps != null)
            {
                for (int j = 0; j < colliderProps.Length; j++)//이거 되는지 확인해야됨.
                {
                    colliderProps[j].enabled = false;
                }
            }
            PoolManager.Instance.EnemyPool.Release(enemies[i]);
        }
    }
}
