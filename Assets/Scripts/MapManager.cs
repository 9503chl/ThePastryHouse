using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    private int CylinderCount;
    private int CubeCount;

    public GameObject Cylinder;
    public GameObject Cube;

    private GameObject prop;

    private List<BoxCollider> boxColliders;
    private List<CapsuleCollider> capsuleColliders;

    private BoxCollider propBoxCollider;
    private CapsuleCollider propCapsureCollider;

    public float MaxX;//960
    public float MaxY;//540

    public float MinX;
    public float MinY;

    private void Awake()
    {
        Instance = this;

        CylinderCount = PoolManager.Instance.CylinderPoolSize;
        CubeCount = PoolManager.Instance.CubePoolSize;
    }

    public void CreateMap(Transform targetTransform)
    {
        for (int i = 0; i < CylinderCount; i++)
        {
            prop = PoolManager.Instance.CylinderPool.Get();
            prop.transform.parent = targetTransform;

            int scaleFactor = Random.Range(0, 3);
            prop.transform.localScale = Vector3.one * scaleFactor * 100;

            prop.transform.localPosition = new Vector3(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY), scaleFactor * -50);//Z 값은 피봇 대신 쓰는거.

            propBoxCollider = prop.GetComponent<BoxCollider>();
            propBoxCollider.size *= 2;
            boxColliders.Add(propBoxCollider);
        }

        for (int i = 0; i < CubeCount; i++)
        {
            prop = PoolManager.Instance.CubePool.Get();
            prop.transform.parent = targetTransform;

            int scaleFactor = Random.Range(0, 3);
            prop.transform.localScale = Vector3.one * scaleFactor;

            prop.transform.localPosition = new Vector3(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY), scaleFactor * -50);//Z 값은 피봇 대신 쓰는거.

            propCapsureCollider = prop.GetComponent<CapsuleCollider>();
            propCapsureCollider.radius *= 2;
            capsuleColliders.Add(propCapsureCollider);
        }
    }
    public void ColliderReset()
    {
        for (int i = 0; i < boxColliders.Count; i++) boxColliders[i].size /= 2;
        for (int i = 0; i < capsuleColliders.Count; i++) capsuleColliders[i].radius /= 2;
    }

}
