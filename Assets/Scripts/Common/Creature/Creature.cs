using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Pool;

public class Creature : MonoBehaviour
{
    public float HP;

    public float Speed;

    public float CurrentHP;

    public float Damage;

    public bool isDamaged = true;

    public SpriteRenderer m_Sprite;

    public Animator m_Animator;

    public string AnimatorTrigger;

    public float animationTime;

    public List<MonoBehaviour> monoList = new List<MonoBehaviour>();


    public virtual void DamageCount(float damage, float damageInterval, Image damageImage)
    {
        if (isDamaged == true)
            StartCoroutine(DamageDelay(damage, damageInterval,  damageImage));
    }
    public virtual void DamageCount(float damage, float damageInterval)
    {
        if (isDamaged == true)
            StartCoroutine(DamageDelay(damage, damageInterval));
    }

    IEnumerator DamageDelay(float damage, float damageInterval, Image DamageImage)
    {
        HPManager.Instance.OnHit(transform, HP, HP - damage);
        isDamaged = false;
        CurrentHP -= damage;
        m_Sprite.color = new Color(m_Sprite.color.r, m_Sprite.color.g, m_Sprite.color.b, 0.5f);
        DamageImage.DOColor(new Color(DamageImage.color.r, DamageImage.color.g, DamageImage.color.b, 0.4f), 0.25f);

        yield return new WaitForSeconds(0.25f);
        DamageImage.DOColor(new Color(DamageImage.color.r, DamageImage.color.g, DamageImage.color.b, 0), 0.25f);

        yield return new WaitForSeconds(damageInterval - 0.25f);
        m_Sprite.color = new Color(m_Sprite.color.r, m_Sprite.color.g, m_Sprite.color.b, 1);
        isDamaged = true;
    }
    IEnumerator DamageDelay(float damage, float damageInterval)
    {
        HPManager.Instance.OnHit(transform, HP, HP - damage);
        isDamaged = false;
        CurrentHP -= damage;
        m_Sprite.color = new Color(m_Sprite.color.r, m_Sprite.color.g, m_Sprite.color.b, 0.5f);

        yield return new WaitForSeconds(damageInterval);
        m_Sprite.color = new Color(m_Sprite.color.r, m_Sprite.color.g, m_Sprite.color.b, 1);
        isDamaged = true;
    }
    public virtual IEnumerator DieCor()
    {
        m_Animator.SetTrigger(AnimatorTrigger);
        yield return new WaitForEndOfFrame();

        animationTime = m_Animator.GetCurrentAnimatorClipInfo(0).Length;
    }

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
        m_Animator = GetComponent<Animator>();
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
    public virtual void ComponentOff(List <MonoBehaviour> monos)
    {
        for(int i = 0; i< monos.Count; i++)
        {
            monos[i].enabled = false;
        }
    }
    public virtual void ComponentOn(List<MonoBehaviour> monos)
    {
        for (int i = 0; i < monos.Count; i++)
        {
            monos[i].enabled = true;
        }
    }
}
