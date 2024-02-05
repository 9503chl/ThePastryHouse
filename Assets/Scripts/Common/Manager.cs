using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Manager : MonoBehaviour
{
    public IObjectPool<GameObject> m_PoolA;
    public IObjectPool<GameObject> m_PoolB;

    public Rigidbody2D RigidBodyProp;

    public List<Rigidbody2D> m_Rigidbody2DList;

    public Collider2D ColliderProp;

    public Vector2 XValue;//960
    public Vector2 YValue;//540

    public GameObject ObjProp;

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
        XValue = new Vector2(-19.2f, 19.2f);
        YValue = new Vector2(-10.8f, 10.8f);
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

    public virtual void CreateProps(Transform targetTransform)
    {

    }
    public virtual void ResetProps()
    {

    }
    public virtual void ComponentOff()
    {

    }
    public virtual void ComponentOn()
    {

    }
    public virtual IEnumerator DelayReset()//맵이 세팅되고 겹치는걸 방지해서 5초간 시간을 줌.
    {
        if(Time.timeScale == 1)
            Time.timeScale = 5.0f;

        float time = 0.8f;
        while (time > 0)
        {
            time -= Time.fixedDeltaTime;
            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime);
        }
        for (int i = 0; i < m_Rigidbody2DList.Count; i++)
        {
            m_Rigidbody2DList[i].bodyType = RigidbodyType2D.Kinematic;
        }
        if(Time.timeScale != 1)
            Time.timeScale = 1.0f;
    }
}
