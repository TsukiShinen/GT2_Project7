using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiWanderState : IState
{
    private SamuraiController _samurai;

    public SamuraiWanderState(SamuraiController samurai)
    {
        _samurai = samurai;
    }

    public IState HandleInput()
    {
        if (_samurai.Detection.IsPlayerDetected) { return _samurai.TargetState; }

        return this;
    }

    public void Update()
    {
        _samurai.Patrol();
    }

    public void FixedUpdate()
    {
        _samurai.FixedPatrol();
    }

    public void Enter()
    {
        _samurai.CanMove = true;
        _samurai.TimerFlip = Random.Range(4f, 8f);
    }

    public void Exit()
    {

    }
}
