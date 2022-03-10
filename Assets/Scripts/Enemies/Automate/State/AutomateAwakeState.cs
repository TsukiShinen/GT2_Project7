using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomateAwakeState : IState
{
    private AutomateController _automate;

    public AutomateAwakeState(AutomateController automate)
    {
        _automate = automate;
    }

    public IState HandleInput()
    {
        if (DayNightManager.Instance.IsDay)
            return _automate.NoneState;
        return _automate.RunState;
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {

    }

    public void Enter()
    {
        _automate.Animator.SetTrigger("Awake");
    }

    public void Exit()
    {

    }
}
