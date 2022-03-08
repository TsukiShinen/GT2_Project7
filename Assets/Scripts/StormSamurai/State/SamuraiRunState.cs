using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiRunState : IState
{
    private SamuraiController _samurai;

    public SamuraiRunState(SamuraiController samurai)
    {
        _samurai = samurai;
    }

    public IState HandleInput()
    {
        if (!_samurai.Detection.IsPlayerDetected)
            return _samurai.ToIdleState;
        else if (Vector2.Distance(_samurai.Detection.PlayerPosition, _samurai.transform.position) <= _samurai.AttackRange && _samurai.AttackTimer <= 0)
            return _samurai.AttackState;
        return this;
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        if (Vector2.Distance(_samurai.Detection.PlayerPosition, _samurai.transform.position) <= _samurai.AttackRange)
        {
            _samurai.Rigidbody.velocity = Vector2.zero;
            _samurai.Animator.SetBool("Run", false);
            return;
        }
        Vector2 dir = new Vector2(_samurai.Detection.PlayerPosition.x - _samurai.transform.position.x, 0).normalized;
        _samurai.SpriteRenderer.flipX = dir == new Vector2(-1, 0);
        _samurai.Rigidbody.AddForce(dir * _samurai.Speed * Time.deltaTime);
        _samurai.Animator.SetBool("Run", true);
    }

    public void Enter()
    {
        _samurai.Animator.SetBool("Run", true);
    }

    public void Exit()
    {

    }
}
