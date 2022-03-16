using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomateWanderState : IState
{
    private AutomateController _automate;

    public AutomateWanderState(AutomateController automate)
    {
        _automate = automate;
    }

    public IState HandleInput()
    {
        if (_automate.Detection.IsPlayerDetected) { return _automate.TargetState; }
        else if (DayNightManager.Instance.IsDay) { return _automate.NoneState; }

        return this;
    }

    public void Update()
    {
        _automate.Patrol();
    }

    public void FixedUpdate()
    {
        _automate.FixedPatrol();
    }

    public void Enter()
    {
        _automate.CanMove = true;
        _automate.TimerFlip = Random.Range(4f, 8f);
    }

    public void Exit()
    {

    }
}
