using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSectorForm : MonoBehaviour
{
    private Vector2 baseVector2 = Vector3.up;

    public int Angle;

    public float Radius;

    public List<Vector2> Vector2List;
    private void CreateCollider()
    {
        baseVector2 *= Radius;

        Vector2List.Add(Vector2.zero);

        for (int i = -Angle / 2; i <= Angle / 2; i++)
        {
            Vector2List.Add(Quaternion.AngleAxis(i, Vector3.forward) * baseVector2);
        }
        Vector2List.Add(Vector2.zero);
    }
    void Start()
    {
        CreateCollider();
    }
}
