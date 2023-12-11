using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEditor;
public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    public int CircleCapacity;
    public int CirclePoolSize;

    private GameObject CirclePrefab;

    public IObjectPool<GameObject> CirclePool { get; private set; }
    public IObjectPool<GameObject> PaperLeftPool { get; private set; }
    public IObjectPool<GameObject> PaperRightPool { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
        Init();
        Application.targetFrameRate = 60;
    }

    private void Init()
    {
        CirclePool = new ObjectPool<GameObject>(CreateCircleItem, OnTakeFromPoolCircle, OnReturnedToPool_Sphere,
        OnDestroyPoolObject, true, CircleCapacity, CirclePoolSize);

        // 미리 오브젝트 생성 해놓기
        for (int i = 0; i < CircleCapacity; i++)
        {
            GameObject circle = CreateCircleItem();
            CirclePool.Release(circle.gameObject);
        }
    }


    // 생성
    private GameObject CreateCircleItem()
    {
        GameObject poolGo = Instantiate(CirclePrefab);
        return poolGo;
    }

    private void OnTakeFromPoolCircle(GameObject poolGo)
    {

    }
    
    // 반환
    private void OnReturnedToPool_Sphere(GameObject poolGo)
    {
        poolGo.transform.localPosition = Vector3.left * 2000;
    }
    // 삭제
    private void OnDestroyPoolObject(GameObject poolGo)
    {
        Destroy(poolGo);
    }
}
