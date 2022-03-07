using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiAttackState : IState
{
    private SamuraiController _samurai;

    private bool _isAttacking;

    public SamuraiAttackState(SamuraiController samurai)
    {
        _samurai = samurai;
    }

    public IState HandleInput()
    {
        if (!_isAttacking)
            return _samurai.IdleState;

        return this;
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {

    }

    public void Enter()
    {
        _isAttacking = true;
        _samurai.StartCoroutine(Attack());
    }

    public void Exit()
    {
        _samurai.AttackTimer = _samurai.AttackCooldown;
    }

    private IEnumerator Attack()
    {
        Vector2 dir = new Vector2(_samurai.Detection.PlayerPosition.x - _samurai.transform.position.x, 0).normalized;
        if (dir == new Vector2(1, 0))
            _samurai.SpriteRenderer.flipX = false;
        else
            _samurai.SpriteRenderer.flipX = true;
        int i = Random.Range(0, 2);
        if (i == 0)
        {
            yield return new WaitForSeconds(0.3f);
            _samurai.AttackBox.SetActive(true);
            _samurai.Animator.SetTrigger("Attack1");
            yield return new WaitForSeconds(0.6f);
            _samurai.AttackBox.SetActive(false);
            _isAttacking = false;
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
            _samurai.AttackBox.SetActive(true);
            _samurai.Animator.SetTrigger("Attack2");
            yield return new WaitForSeconds(0.6f);
            _samurai.AttackBox.SetActive(false);
            _isAttacking = false;
        }
        
    }
}
