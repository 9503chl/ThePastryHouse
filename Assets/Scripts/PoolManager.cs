using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEditor;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;

    public int CubeCapacity;
    public int CubePoolSize;

    public int CylinderCapacity;
    public int CylinderPoolSize;

    public GameObject CubePrefab;
    public GameObject CylinderPrefab;

    public IObjectPool<GameObject> CubePool { get; private set; }
    public IObjectPool<GameObject> CylinderPool { get; private set; }

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
        CubePool = new ObjectPool<GameObject>(CreateCircleItem, OnTakeFromPool, OnReturnedToPool,
        OnDestroyPoolObject, true, CubeCapacity, CubePoolSize);

        CylinderPool = new ObjectPool<GameObject>(CreateCircleItem, OnTakeFromPool, OnReturnedToPool,
        OnDestroyPoolObject, true, CubeCapacity, CubePoolSize);

        // �̸� ������Ʈ ���� �س���
        for (int i = 0; i < CubeCapacity; i++)
        {
            GameObject Box = CreateCircleItem();
            CubePool.Release(Box.gameObject);
        }


        for (int i = 0; i < CylinderCapacity; i++)
        {
            GameObject Cylinder = CreateCircleItem();
            CylinderPool.Release(Cylinder.gameObject);
        }
    }


    // ����
    private GameObject CreateCircleItem()
    {
        GameObject poolGo = Instantiate(CubePrefab);
        return poolGo;
    }

    private void OnTakeFromPool(GameObject poolGo)
    {
            
    }
    
    // ��ȯ
    private void OnReturnedToPool(GameObject poolGo)
    {
        poolGo.transform.localPosition = Vector3.left * 3000;
    }
    // ����
    private void OnDestroyPoolObject(GameObject poolGo)
    {
        Destroy(poolGo);
    }
}
