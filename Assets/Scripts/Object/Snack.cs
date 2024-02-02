using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snack : Object
{
    public int Damage;

    private Player playerProp;

    private Animator animator;

    public string AnimatorTrigger;

    private Coroutine coroutine;

    public override void OnAwake()
    {
        base.OnAwake();
        Damage = GameSetting.Instance.CurrentMissionData.SnackValue;
        animator = GetComponent<Animator>();
    }


    public override void CollisionEnterOn(Collision2D collision)
    {
        if (coroutine == null) {
            if (collision.transform.tag == "Player")
            {
                playerProp = collision.gameObject.GetComponent<Player>();
                if (playerProp != null)
                {
                    playerProp.DamageCount(-Damage, playerProp.damageInterval, playerProp.DamageImage);
                    HPManager.Instance.HpText.text = playerProp.CurrentHP.ToString();
                    coroutine = StartCoroutine(HealNRelease());
                }
            }
        }
    }
    private IEnumerator HealNRelease()
    {
        animator.Play(AnimatorTrigger);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);

        gameObject.transform.position = PoolManager.Instance.VectorAway;
        PoolManager.Instance.SnackPool.Release(gameObject);
        coroutine = null;
    }
}
