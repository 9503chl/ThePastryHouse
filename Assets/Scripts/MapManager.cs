using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    public GameObject Cylinder;
    public GameObject Cube;

    public float MaxX;//960
    public float MaxY;

    public float MinX;//540
    public float MinY;

    private List <GameObject> cylinderList = new List<GameObject>();
    private List <GameObject> cubeList = new List<GameObject>();

    private void Awake()
    {
        Instance = this;    
    }

    public void CreateMap(Transform targetTransform)
    {

    }
}
