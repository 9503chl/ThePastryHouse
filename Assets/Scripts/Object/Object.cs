using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
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
    private void FixedUpdate()
    {
        OnFixedUpdate();
    }
    void OnEnable()
    {
        EnableOn();
    }
    void OnDisable()
    {
        DisableOn();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionEnterOn(collision);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        CollisionExitOn(collision);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        CollisionStayOn(collision);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        TriggerEnterOn(collider);
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        TriggerExitOn(collider);
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        TriggerStayOn(collider);
    }
    public virtual void OnFixedUpdate()
    {

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
    public virtual void TriggerEnterOn(Collider2D collider)
    {

    }
    public virtual void TriggerExitOn(Collider2D collider)
    {

    }
    public virtual void TriggerStayOn(Collider2D collider)
    {

    }
    public virtual void CollisionEnterOn(Collision2D collision)
    {

    }
    public virtual void CollisionExitOn(Collision2D collision)
    {

    }
    public virtual void CollisionStayOn(Collision2D collision)
    {

    }
}
