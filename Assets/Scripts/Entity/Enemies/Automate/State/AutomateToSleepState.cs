using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomateToSleepState : IState
{
    private AutomateController _automate;

    public AutomateToSleepState(AutomateController automate)
    {
        _automate = automate;
    }

    public IState HandleInput()
    {
        if (_automate.Rigidbody.velocity == Vector2.zero)
            return _automate.SleepState;
        else if (_automate.Detection.IsPlayerDetected)
            return _automate.RunState;

        return this;
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        _automate.Rigidbody.velocity = Vector2.zero;
    }

    public void Enter()
    {

    }

    public void Exit()
    {
        _automate.Animator.SetTrigger("Sleep");
    }
}
