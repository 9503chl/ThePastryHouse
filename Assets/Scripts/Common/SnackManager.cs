using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SnackManager : Manager
{
    public static SnackManager Instance;

    private List<Snack> snackList = new List<Snack>();

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
    public void SnackComponentOff()
    {
        for (int i = 0; i < snackList.Count; i++)
        {
            snackList[i].GetComponent<Enemy>().enabled = false;
            ColliderProp = ObjProp.GetComponent<Collider2D>();
            if (ColliderProp != null)
            {
                ColliderProp.enabled = false;
            }
        }
    }
    public void SnackComponentOn()
    {
        for (int i = 0; i < snackList.Count; i++)
        {
            snackList[i].GetComponent<Enemy>().enabled = true; ;
            ColliderProp = ObjProp.GetComponent<Collider2D>();
            if (ColliderProp != null)
            {
                ColliderProp.enabled = true;
            }
        }
    }

    public override void ResetProp()
    {
        snackList.RemoveRange(0, snackList.Count);
    }
}
