using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : IState
{
    Player _player;

    private bool _isAttacking;

    public PlayerAttackState(Player player)
    {
        _player = player;
    }

    public IState HandleInput()
    {
        if (!_isAttacking) { return _player.IdleState; }

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
        _player.Rigidbody.velocity = Vector3.zero;
        _isAttacking = true;
        _player.Animator.SetTrigger("Attack");
        _player.StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.3f);
        _player.AttackBox.enabled = true;
        yield return new WaitForSeconds(0.4f);
        _player.AttackBox.enabled = false;

        _isAttacking = false;
    }

    public void Exit()
    {

    }
}
