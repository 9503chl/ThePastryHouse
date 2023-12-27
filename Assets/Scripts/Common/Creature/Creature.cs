using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public float HP;

    public float Speed;

    public float CurrentHP;

    public float Damage;

    public SpriteRenderer m_Sprite;

    private void Reset()
    {
        OnReset();
    }
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
    private void OnCollisionEnter(Collision collision)
    {
        CollisionEnterOn(collision);
    }
    private void OnCollisionExit(Collision collision)
    {
        CollisionExitOn(collision);
    }
    private void OnCollisionStay(Collision collision)
    {
        CollisionStayOn(collision);
    }
    private void OnTriggerEnter(Collider collider)
    {
        TriggerEnterOn(collider);
    }
    private void OnTriggerExit(Collider collider)
    {
        TriggerExitOn(collider);
    }
    private void OnTriggerStay(Collider collider)
    {
        TriggerStayOn(collider);
    }
    public virtual void OnReset()
    {

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
    public virtual void TriggerEnterOn(Collider collider)
    {

    }
    public virtual void TriggerExitOn(Collider collider)
    {

    }
    public virtual void TriggerStayOn(Collider collider)
    {

    }
    public virtual void CollisionEnterOn(Collision collision)
    {

    }
    public virtual void CollisionExitOn(Collision collision)
    {

    }
    public virtual void CollisionStayOn(Collision collision)
    {

    }
}
