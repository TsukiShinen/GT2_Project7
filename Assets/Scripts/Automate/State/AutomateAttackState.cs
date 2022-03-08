using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomateAttackState : IState
{
    private AutomateController _automate;

    private bool _isAttacking;

    public AutomateAttackState(AutomateController automate)
    {
        _automate = automate;
    }

    public IState HandleInput()
    {
        if (!_isAttacking)
            return _automate.RunState;
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
        _automate.StartCoroutine(Attack());
    }

    public void Exit()
    {
        _automate.AttackTimer = _automate.AttackCooldown;
    }

    private IEnumerator Attack()
    {
        Vector2 dir = new Vector2(_automate.Detection.PlayerPosition.x - _automate.transform.position.x, 0).normalized;
        _automate.SpriteRenderer.flipX = dir == new Vector2(-1, 0);
        yield return new WaitForSeconds(0.3f);
        _automate.Animator.SetTrigger("Charge");
        _isAttacking = false;
    }
}
