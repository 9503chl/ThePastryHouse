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

    public GameObject BoxPrefab;
    public GameObject CirclePrefab;

    public Transform ObjectTf;


    public IObjectPool<GameObject> BoxPool { get; private set; }
    public IObjectPool<GameObject> CirclePool { get; private set; }

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
        BoxPool = new ObjectPool<GameObject>(CreateCircleItem_Box, OnTakeFromPool, OnReturnedToPool,
        OnDestroyPoolObject, true, BoxCapacity, BoxPoolSize);

        CirclePool = new ObjectPool<GameObject>(CreateCircleItem_Circle, OnTakeFromPool, OnReturnedToPool,
        OnDestroyPoolObject, true, BoxCapacity, BoxPoolSize);

        // 미리 오브젝트 생성 해놓기
        for (int i = 0; i < BoxCapacity; i++)
        {
            GameObject Box = CreateCircleItem_Box();
            BoxPool.Release(Box.gameObject);
        }


        for (int i = 0; i < CircleCapacity; i++)
        {
            GameObject Circle = CreateCircleItem_Circle();
            CirclePool.Release(Circle.gameObject);
        }
    }


    // 생성
    private GameObject CreateCircleItem_Box()
    {
        GameObject poolGo = Instantiate(BoxPrefab, ObjectTf);
        return poolGo;
    }
    private GameObject CreateCircleItem_Circle()
    {
        GameObject poolGo = Instantiate(CirclePrefab, ObjectTf);
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
        Destroy(poolGo);
    }
}
