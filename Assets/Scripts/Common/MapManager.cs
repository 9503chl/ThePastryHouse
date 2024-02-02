using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Manager
{
    public static MapManager Instance;

    public List<GameObject> CircleList= new List<GameObject>();
    public List<GameObject> BoxList = new List<GameObject>();

    private SaveData saveData;


    public override void OnAwake()
    {
        base.OnAwake();
        Instance = this;

        m_PoolA = PoolManager.Instance.CirclePool;
        m_PoolB = PoolManager.Instance.BoxPool;
    }

    public override void CreateProps(Transform targetTransform)
    {
        if (GameSetting.Instance.CurrentSaveData.IsFirst)
        {
            for (int i = 0; i < PoolManager.Instance.CirclePoolSize; i++)
            {
                ObjProp = m_PoolA.Get();
                ObjProp.transform.SetParent(targetTransform);

                int scaleFactor = Random.Range(1, 3);
                ObjProp.transform.localScale = Vector3.one * scaleFactor * 10;

                CircleList.Add(ObjProp);

                RigidBodyProp = ObjProp.GetComponent<Rigidbody2D>();
                if (RigidBodyProp != null)
                {
                    ObjProp.transform.localPosition = new Vector2(Random.Range(XValue.x, XValue.y) * 10, Random.Range(YValue.x, YValue.y) * 10);
                    RigidBodyProp.bodyType = RigidbodyType2D.Dynamic;
                    m_Rigidbody2DList.Add(RigidBodyProp);
                }
            }

            for (int i = 0; i < PoolManager.Instance.BoxPoolSize; i++)
            {
                ObjProp = m_PoolB.Get();
                ObjProp.transform.SetParent(targetTransform);

                int scaleFactor = Random.Range(1, 3);
                ObjProp.transform.localScale = Vector3.one * scaleFactor * 10;

                BoxList.Add(ObjProp);

                RigidBodyProp = ObjProp.GetComponent<Rigidbody2D>();
                if (RigidBodyProp != null)
                {
                    ObjProp.transform.localPosition = new Vector2(Random.Range(XValue.x, XValue.y) * 10, Random.Range(YValue.x, YValue.y) * 10);
                    m_Rigidbody2DList.Add(RigidBodyProp);
                }
            }
        }
        else
        {
            saveData = GameSetting.Instance.CurrentSaveData;

            for (int i = 0; i < saveData.CirclePositionXs.Count; i++)
            {
                ObjProp = m_PoolA.Get();
                ObjProp.transform.SetParent(targetTransform);

                ObjProp.transform.localScale = Vector3.one * saveData.CircleScales[i];

                CircleList.Add(ObjProp);

                RigidBodyProp = ObjProp.GetComponent<Rigidbody2D>();
                if (RigidBodyProp != null)
                {
                    ObjProp.transform.localPosition = new Vector3(saveData.CirclePositionXs[i], saveData.CirclePositionYs[i], 0);
                    RigidBodyProp.bodyType = RigidbodyType2D.Dynamic;
                    m_Rigidbody2DList.Add(RigidBodyProp);
                }
            }
            for (int i = 0; i < saveData.BoxPositionXs.Count; i++)
            {
                ObjProp = m_PoolB.Get();
                ObjProp.transform.SetParent(targetTransform);

                ObjProp.transform.localScale = Vector3.one * saveData.BoxScales[i];

                CircleList.Add(ObjProp);

                RigidBodyProp = ObjProp.GetComponent<Rigidbody2D>();
                if (RigidBodyProp != null)
                {
                    ObjProp.transform.localPosition = new Vector3(saveData.BoxPositionXs[i], saveData.BoxPositionYs[i], 0);
                    RigidBodyProp.bodyType = RigidbodyType2D.Dynamic;
                    m_Rigidbody2DList.Add(RigidBodyProp);
                }
            }
        }
        StartCoroutine(DelayReset());
    }

    public override void ResetProp()
    {
        CircleList.RemoveRange(0, CircleList.Count);
        BoxList.RemoveRange(0, CircleList.Count);
    }
}
