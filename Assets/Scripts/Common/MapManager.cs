using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    private GameObject prop;

    public List<Rigidbody2D> Rigidbody2DList = new List<Rigidbody2D>();

    public List<GameObject> CircleList= new List<GameObject>();
    public List<GameObject> BoxList = new List<GameObject>();

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
            prop.transform.localScale = Vector3.one * scaleFactor * 10;

            CircleList.Add(prop);

            tempRigid2D = prop.GetComponent<Rigidbody2D>();
            if(tempRigid2D != null)
            {
                prop.transform.localPosition = new Vector3(Random.Range(MinX, MaxX) * 10, Random.Range(MinY, MaxY) * 10, 0);//Z 값은 피봇 대신 쓰는거.
                tempRigid2D.bodyType = RigidbodyType2D.Dynamic;
                Rigidbody2DList.Add(tempRigid2D);
            }
        }

        for (int i = 0; i < cubeCount; i++)
        {
            prop = PoolManager.Instance.BoxPool.Get();
            prop.transform.SetParent(targetTransform);

            int scaleFactor = Random.Range(1, 3);
            prop.transform.localScale = Vector3.one * scaleFactor * 10;

            BoxList.Add(prop);

            tempRigid2D = prop.GetComponent<Rigidbody2D>();
            if (tempRigid2D != null)
            {
                prop.transform.localPosition = new Vector3(Random.Range(MinX, MaxX) * 10, Random.Range(MinY, MaxY) * 10, 0);//Z 값은 피봇 대신 쓰는거.
                Rigidbody2DList.Add(tempRigid2D);
            }
        }
        StartCoroutine(DelayReset());
    }
    public void BoxNCubeReset()
    {
        CircleList.RemoveRange(0, CircleList.Count);
        BoxList.RemoveRange(0, CircleList.Count);
    }
    IEnumerator DelayReset()//맵이 세팅되고 겹치는걸 방지해서 5초간 시간을 줌.
    {
        Time.timeScale = 5.0f;
        float time = 0.8f;
        while (time > 0)
        {
            time -= Time.fixedDeltaTime;
            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
        for (int i = 0; i < Rigidbody2DList.Count; i++)
        {
            Rigidbody2DList[i].bodyType = RigidbodyType2D.Kinematic;
        }
        Time.timeScale = 1.0f;
    }
}
