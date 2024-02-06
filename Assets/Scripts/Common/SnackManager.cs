using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SnackManager : Manager
{
    public static SnackManager Instance;

    public List<GameObject> SnackList = new List<GameObject>();

    public override void OnAwake()
    {
        base.OnAwake();
        Instance = this;
        m_PoolA = PoolManager.Instance.SnackPool;
    }

    public override void CreateProps(Transform targetTransform)
    {
        for (int i = 0; i < GameSetting.Instance.CurrentSaveData.RemainSnackCount; i++)
        {
            ObjProp = m_PoolA.Get();
            ObjProp.transform.SetParent(targetTransform);

            SnackList.Add(ObjProp);

            RigidBodyProp = ObjProp.GetComponent<Rigidbody2D>();
            if (RigidBodyProp != null)
            {
                ObjProp.transform.localPosition = new Vector2(Random.Range(XValue.x, XValue.y) * 10, Random.Range(YValue.x, YValue.y) * 10);
                RigidBodyProp.bodyType = RigidbodyType2D.Dynamic;
                m_Rigidbody2DList.Add(RigidBodyProp);
            }
        }
        StartCoroutine(DelayReset());
    }
    public override void ComponentOn()
    {
        for (int i = 0; i < SnackList.Count; i++)
        {
            SnackList[i].GetComponent<Snack>().enabled = true;
            ColliderProp = ObjProp.GetComponent<Collider2D>();
            if (ColliderProp != null)
            {
                ColliderProp.enabled = true;
            }
        }
    }
    public override void ComponentOff()
    {
        for (int i = 0; i < SnackList.Count; i++)
        {
            SnackList[i].GetComponent<Snack>().enabled = false;
            ColliderProp = ObjProp.GetComponent<Collider2D>();
            if (ColliderProp != null)
            {
                ColliderProp.enabled = false;
            }
        }
    }

    public override void ResetProps()
    {
        for (int i = 0; i < SnackList.Count; i++)
        {
            m_PoolA.Release(SnackList[i].gameObject);
        }
        SnackList.RemoveRange(0, SnackList.Count);
    }
}
