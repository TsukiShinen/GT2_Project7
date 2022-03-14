using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomateControlState : IState
{
    private AutomateController _automate;

    public AutomateControlState(AutomateController automate)
    {
        _automate = automate;
    }

    public IState HandleInput()
    {
        return this;
    }

    public void Update()
    {
        _automate.transform.position = _automate.transform.Find("Player").position;
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
