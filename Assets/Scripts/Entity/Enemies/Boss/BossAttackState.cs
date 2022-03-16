using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackState : IState
{
    private BossController _boss;

    private bool _isAttacking;

    public BossAttackState(BossController boss)
    {
        _boss = boss;
    }

    public IState HandleInput()
    {
        if (!_isAttacking) { return _boss.TargetState; }

        return this;
    }

    public void Update()
    {
        if (Mathf.Sign((_boss.Detection.PlayerPosition - _boss.transform.position).x) != Mathf.Sign(_boss.transform.GetChild(0).localScale.x) && _boss.Rigidbody.velocity.x != 0)
        {
            _boss.Flip();
        }
    }

    public void FixedUpdate()
    {

    }

    public void Enter()
    {
        _isAttacking = true;
        _boss.StartCoroutine(Attack());
    }

    public IEnumerator Attack()
    {
        _boss.Animator.SetTrigger("Charge");
        yield return new WaitForSeconds(1f);
        _boss.Animator.SetTrigger("Attack");
        _boss.FirstAttackBox.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        _boss.FirstAttackBox.SetActive(false);
        _boss.SecondAttackBox.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        _boss.SecondAttackBox.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        _isAttacking = false;
    }

    public void Exit()
    {

    }
}
