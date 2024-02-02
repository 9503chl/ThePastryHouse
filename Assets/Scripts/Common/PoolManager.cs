using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEditor;
using UnityEngine.UI;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    public int BoxCapacity;
    public int BoxPoolSize;

    public int CircleCapacity;
    public int CirclePoolSize;

    public int EnemyCapacity;
    public int EnemyPoolSize;

    public int HPBarCapacity;
    public int HPBarPoolSize;

    public int SnackCapacity;
    public int SnackPoolSize;

    public GameObject BoxPrefab;
    public GameObject CirclePrefab;
    public GameObject EnemyPrefab;
    public GameObject HPBarPrefab;
    public GameObject SnackPrefab;

    public Transform ObjectTf;

    public Vector3 VectorAway;

    private Enemy enemyProp;
    private Image imageProp;


    public IObjectPool<GameObject> BoxPool { get; private set; }
    public IObjectPool<GameObject> CirclePool { get; private set; }
    public IObjectPool<GameObject> EnemyPool { get; private set; }
    public IObjectPool<GameObject> HPBarPool { get; private set; }
    public IObjectPool<GameObject> SnackPool { get; private set; }


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
        BoxPool = new UnityEngine.Pool.ObjectPool<GameObject>(CreateBox, OnTakeFromPool, OnReturnedToPool,
        OnDestroyPoolObject, true, BoxCapacity, BoxPoolSize);

        CirclePool = new UnityEngine.Pool.ObjectPool<GameObject>(CreateCircle, OnTakeFromPool, OnReturnedToPool,
        OnDestroyPoolObject, true, CircleCapacity, CirclePoolSize);

        EnemyPool = new UnityEngine.Pool.ObjectPool<GameObject>(CreateEnemy, OnTakeFromPool, OnReturnedToPool,
        OnDestroyPoolObject, true, EnemyCapacity, EnemyPoolSize);

        HPBarPool = new UnityEngine.Pool.ObjectPool<GameObject>(CreateHPBox, OnTakeFromPool, OnReturnedToPool,
        OnDestroyPoolObject, true, EnemyCapacity, EnemyPoolSize);

        SnackPool = new UnityEngine.Pool.ObjectPool<GameObject>(CreateSnack, OnTakeFromPool, OnReturnedToPool,
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
        for (int i = 0; i < HPBarCapacity; i++)
        {
            GameObject HPBar = CreateHPBox();
            HPBarPool.Release(HPBar);
        }
        for (int i = 0; i < SnackCapacity; i++)
        {
            GameObject Snack = CreateSnack();
            SnackPool.Release(Snack);
        }

        PlayerInput.PlayerInputInstance.sight2Ds = FindObjectsOfType<Sight2D>();
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
    private GameObject CreateHPBox()
    {
        GameObject poolGo = Instantiate(HPBarPrefab, ObjectTf);
        return poolGo;
    }
    private GameObject CreateSnack()
    {
        GameObject poolGo = Instantiate(SnackPrefab, ObjectTf);
        return poolGo;
    }
    private void OnTakeFromPool(GameObject poolGo)
    {

    }
    
    // 반환
    private void OnReturnedToPool(GameObject poolGo)
    {
        poolGo.transform.position = VectorAway;

        enemyProp = poolGo.GetComponent<Enemy>();
        if(enemyProp != null )
        {
            enemyProp.enabled = false;
            enemyProp.FullHP();
        }
    }
    // 삭제
    private void OnDestroyPoolObject(GameObject poolGo)
    {

    }
}
