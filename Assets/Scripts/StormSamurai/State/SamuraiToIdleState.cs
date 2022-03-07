using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiToIdleState : IState
{
    private SamuraiController _samurai;

    public SamuraiToIdleState(SamuraiController samurai)
    {
        _samurai = samurai;
    }

    public IState HandleInput()
    {
        if (_samurai.Rigidbody.velocity == Vector2.zero)
            return _samurai.IdleState;
        if (_samurai.Detection.IsPlayerDetected)
            return _samurai.RunState;

        return this;
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        _samurai.Rigidbody.velocity = Vector2.zero;
    }

    public void Enter()
    {
        
    }

    public void Exit()
    {
        _samurai.Animator.SetBool("Run", false);
    }
}
