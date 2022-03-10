using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistanceAttack : IState
{
    Player _player;

    private bool _isAttacking;

    public PlayerDistanceAttack(Player player)
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
        _player.Rigidbody.velocity = Vector3.zero;
    }

    public void FixedUpdate()
    {

    }

    public void Enter()
    {
        _isAttacking = true;
        _player.Rigidbody.velocity = Vector3.zero;
        _player.Animator.SetTrigger("Distance");
        _player.StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.1f);
        _player.DistanceAttackBox.enabled = true;
        yield return new WaitForSeconds(0.2f);
        _player.DistanceAttackBox2.enabled = true;
        yield return new WaitForSeconds(0.2f);
        _player.DistanceAttackBox.enabled = false;
        yield return new WaitForSeconds(0.1f);
        _player.DistanceAttackBox2.enabled = false;

        _isAttacking = false;
    }

    public void Exit()
    {

    }
}
