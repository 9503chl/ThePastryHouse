using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    private GameObject prop;
    private Collider2D[] colliderProps;

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
            prop.transform.localPosition = new Vector3(Random.Range(MinX, MaxX) * 10, Random.Range(MinY, MaxY) * 10, 0);
            prop.transform.localRotation = Quaternion.Euler(0, 0, 0);

            enemies.Add(prop);
        }
        EnemyComponentOff();
    }
    public void EnemyComponentOff()
    {
        for(int i= 0; i< enemies.Count; i++) 
        {
            enemies[i].GetComponent<Enemy>().enabled = false;
            colliderProps = prop.GetComponents<Collider2D>();
            if (colliderProps != null)
            {
                for (int j = 0; j < colliderProps.Length; j++)//이거 되는지 확인해야됨.
                {
                    colliderProps[j].enabled = false;
                }
            }
        }
    }
    public void EnemyComponentOn()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<Enemy>().enabled = true;
            colliderProps = prop.GetComponents<Collider2D>();
            if (colliderProps != null)
            {
                for (int j = 0; j < colliderProps.Length; j++)//이거 되는지 확인해야됨.
                {
                    colliderProps[j].enabled = true;
                }
            }
        }
    }

    public void EnemyDie(Enemy enemy, Collider2D collider2D)//회수
    {
        enemy.enabled = false;
        collider2D.enabled = false;
        enemy.transform.position = PoolManager.Instance.VectorAway;
        PoolManager.Instance.EnemyPool.Release(enemy.gameObject);
    }
}
