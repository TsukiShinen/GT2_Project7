using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomateTargetState : IState
{
    private AutomateController _automate;

    public AutomateTargetState(AutomateController automate)
    {
        _automate = automate;
    }

    public IState HandleInput()
    {
        if (!_automate.Detection.IsPlayerDetected) { return _automate.WanderState; }
        else if (Vector2.Distance(_automate.Detection.PlayerPosition, _automate.transform.position) <= _automate.Range) { return _automate.AttackState; }
        else if (DayNightManager.Instance.IsDay) { return _automate.NoneState; }  

        return this;
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        _automate.Target();
    }

    public void Enter()
    {

    }

    public void Exit()
    {
        _automate.Animator.SetBool("Run", false);
    }
}
