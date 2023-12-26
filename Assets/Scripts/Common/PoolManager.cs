using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEditor;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    public int BoxCapacity;
    public int BoxPoolSize;

    public int CircleCapacity;
    public int CirclePoolSize;

    public int EnemyCapacity;
    public int EnemyPoolSize;

    public GameObject BoxPrefab;
    public GameObject CirclePrefab;
    public GameObject EnemyPrefab;

    public Transform ObjectTf;

    public IObjectPool<GameObject> BoxPool { get; private set; }
    public IObjectPool<GameObject> CirclePool { get; private set; }
    public IObjectPool<GameObject> EnemyPool { get; private set; }


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
        Init();
    }

    private void Init()
    {
        BoxPool = new ObjectPool<GameObject>(CreateBox, OnTakeFromPool, OnReturnedToPool,
        OnDestroyPoolObject, true, BoxCapacity, BoxPoolSize);

        CirclePool = new ObjectPool<GameObject>(CreateCircle, OnTakeFromPool, OnReturnedToPool,
        OnDestroyPoolObject, true, CircleCapacity, CirclePoolSize);

        EnemyPool = new ObjectPool<GameObject>(CreateEnemy, OnTakeFromPool, OnReturnedToPool,
     OnDestroyPoolObject, true, EnemyCapacity, EnemyPoolSize);


        // 미리 오브젝트 생성 해놓기
        for (int i = 0; i < BoxCapacity; i++)
        {
            GameObject Box = CreateBox();
            BoxPool.Release(Box);
        }


        for (int i = 0; i < CircleCapacity; i++)
        {
            GameObject Circle = CreateCircle();
            CirclePool.Release(Circle);
        }

        for (int i = 0; i < EnemyCapacity; i++)
        {
            GameObject Enemy = CreateEnemy();
            EnemyPool.Release(Enemy);
        }
    }


    // 생성
    private GameObject CreateBox()
    {
        GameObject poolGo = Instantiate(BoxPrefab, ObjectTf);
        return poolGo;
    }
    private GameObject CreateCircle()
    {
        GameObject poolGo = Instantiate(CirclePrefab, ObjectTf);
        return poolGo;
    }
    private GameObject CreateEnemy()
    {
        GameObject poolGo = Instantiate(EnemyPrefab, ObjectTf);
        return poolGo;
    }

    private void OnTakeFromPool(GameObject poolGo)
    {

    }
    
    // 반환
    private void OnReturnedToPool(GameObject poolGo)
    {
        poolGo.transform.localPosition = Vector3.left * 5000;//화면 밖으로
    }
    // 삭제
    private void OnDestroyPoolObject(GameObject poolGo)
    {
        //Destroy(poolGo);
    }
}
