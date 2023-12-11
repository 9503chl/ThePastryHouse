using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BaseInput : MonoBehaviour
{
    public static BaseInput Instance;

    public GameObject Player;
    void Start()
    {
        OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdate();
    }
    void Awake()
    {
        OnAwake();
    }
    void OnEnable()
    {
        EnableOn();
    }

    public virtual void OnUpdate()
    {

    }
    public virtual void OnStart()
    {

    }

    public virtual void OnAwake()
    {
        
    }
    public virtual void EnableOn()
    {

    }
}
