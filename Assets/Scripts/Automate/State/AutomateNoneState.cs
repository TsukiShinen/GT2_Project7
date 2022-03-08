using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomateNoneState : IState
{
    private AutomateController _automate;

    public AutomateNoneState(AutomateController automate)
    {
        _automate = automate;
    }

    public IState HandleInput()
    {
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
