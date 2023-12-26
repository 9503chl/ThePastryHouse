using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    public GameObject Cylinder;
    public GameObject Cube;

    private GameObject prop;

    private List<Rigidbody> rigidbodies = new List<Rigidbody>();
    private List<Rigidbody2D> rigidbody2Ds = new List<Rigidbody2D>();

    private Rigidbody tempRigid;
    private Rigidbody2D tempRigid2D;

    private int circleCount;
    private int cubeCount;

    public float MaxX;//960
    public float MaxY;//540

    public float MinX;
    public float MinY;

    private void Awake()
    {
        Instance = this;

        circleCount = PoolManager.Instance.CirclePoolSize;
        cubeCount = PoolManager.Instance.BoxPoolSize;
    }

    public void CreateMap(Transform targetTransform)
    {
        for (int i = 0; i < circleCount; i++)
        {
            prop = PoolManager.Instance.CirclePool.Get();
            prop.transform.SetParent(targetTransform);

            int scaleFactor = Random.Range(1, 3);
            prop.transform.localScale = Vector3.one * scaleFactor * 100;

            tempRigid = prop.GetComponent<Rigidbody>();
            tempRigid2D = prop.GetComponent<Rigidbody2D>();

            if(tempRigid != null)//3D 일때
            {
                prop.transform.localPosition = new Vector3(Random.Range(MinX, MaxX) * 100, Random.Range(MinY, MaxY) * 100, scaleFactor * -100);//Z 값은 피봇 대신 쓰는거.
                //prop.transform.localPosition = new Vector3(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY), 0);//Z 값은 피봇 대신 쓰는거.
                tempRigid.isKinematic = false;
                rigidbodies.Add(tempRigid);
            }
            if(tempRigid2D != null)
            {
                prop.transform.localPosition = new Vector3(Random.Range(MinX, MaxX) * 100, Random.Range(MinY, MaxY) * 100, 0);//Z 값은 피봇 대신 쓰는거.
                tempRigid2D.isKinematic = false;
                rigidbody2Ds.Add(tempRigid2D);
            }
        }

        for (int i = 0; i < cubeCount; i++)
        {
            prop = PoolManager.Instance.BoxPool.Get();
            prop.transform.SetParent(targetTransform);

            int scaleFactor = Random.Range(1, 3);
            prop.transform.localScale = Vector3.one * scaleFactor * 100;

            tempRigid = prop.GetComponent<Rigidbody>();
            tempRigid2D = prop.GetComponent<Rigidbody2D>();

            if (tempRigid != null)//3D 일때
            {
                prop.transform.localPosition = new Vector3(Random.Range(MinX, MaxX) * 100, Random.Range(MinY, MaxY) * 100, scaleFactor * -50);//Z 값은 피봇 대신 쓰는거.
                rigidbodies.Add(tempRigid);
            }
            if (tempRigid2D != null)
            {
                prop.transform.localPosition = new Vector3(Random.Range(MinX, MaxX) * 100, Random.Range(MinY, MaxY) * 100, 0);//Z 값은 피봇 대신 쓰는거.
                rigidbody2Ds.Add(tempRigid2D);
            }
        }
        StartCoroutine(DelayReset());
    }
    IEnumerator DelayReset()
    {
        Time.timeScale = 5.0f;
        float time = 0.8f;
        while (time > 0)
        {
            time -= Time.fixedDeltaTime;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        for (int i = 0; i < rigidbodies.Count; i++)
        {
            rigidbodies[i].isKinematic = true;
        }
        for (int i = 0; i < rigidbody2Ds.Count; i++)
        {
            rigidbody2Ds[i].isKinematic = true;
        }
        Time.timeScale = 1.0f;
    }
}
