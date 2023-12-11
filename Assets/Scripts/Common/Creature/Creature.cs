using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public float HP;
    public float Speed;

    void Start()
    {
        OnStart();
    }

    void Awake()
    {
        OnAwake();
    }
    // Update is called once per frame
    void Update()
    {
        OnUpdate();
    }
    void OnEnable()
    {
        EnableOn();
    }
    void OnDisable()
    {
        DisableOn();
    }
    void OnTriggerEnter()
    {
        TriggerEnterOn();
    }
    void OnTriggerExit()
    {
        TriggerExitOn();
    }
    void OnCollisionEnter()
    {
        CollisionEnterOn();
    }
    void OnCollsionExit()
    {
        CollisionExitOn();
    }
    public virtual void OnAwake()
    {

    }

    public virtual void OnStart()
    {

    }
    public virtual void OnUpdate()
    {

    }
    public virtual void EnableOn()
    {

    }
    public virtual void DisableOn()
    {

    }
    public virtual void TriggerEnterOn()
    {

    }
    public virtual void TriggerExitOn()
    {

    }
    public virtual void CollisionEnterOn()
    {

    }
    public virtual void CollisionExitOn()
    {

    }
}
