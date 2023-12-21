using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BaseInput : MonoBehaviour
{
    public static BaseInput BaseInputInstance;

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
    private void FixedUpdate()
    {
        OnFixedUpdate();
    }
    public virtual void OnFixedUpdate()
    {

    }
    public virtual void OnUpdate()
    {

    }
    public virtual void OnStart()
    {

    }

    public virtual void OnAwake()
    {
        BaseInputInstance = this;
    }
    public virtual void EnableOn()
    {

    }
}
