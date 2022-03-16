using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiTargetState : IState
{
    private SamuraiController _samurai;

    public SamuraiTargetState(SamuraiController samurai)
    {
        _samurai = samurai;
    }

    public IState HandleInput()
    {
        if (!_samurai.Detection.IsPlayerDetected) { return _samurai.WanderState; }
        else if (Vector2.Distance(_samurai.Detection.PlayerPosition, _samurai.transform.position) <= _samurai.Range) { return _samurai.AttackState; }

        return this;
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        _samurai.Target();
    }

    public void Enter()
    {

    }

    public void Exit()
    {
        _samurai.Animator.SetBool("Run", false);
    }
}
