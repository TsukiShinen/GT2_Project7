using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomateSleepState : IState
{
    private AutomateController _automate;

    public AutomateSleepState(AutomateController automate)
    {
        _automate = automate;
    }

    public IState HandleInput()
    {
        Debug.Log(DayNightManager.Instance.IsDay);
        if (_automate.Detection.IsPlayerDetected)
            return _automate.AwakeState;
        else if (DayNightManager.Instance.IsDay)
            return _automate.NoneState;

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
        
    }

    public void Exit()
    {

    }
}
