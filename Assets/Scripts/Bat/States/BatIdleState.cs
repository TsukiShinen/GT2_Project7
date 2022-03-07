using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatIdleState : IState
{
    private BatController _bat;

    public BatIdleState(BatController bat)
    {
        _bat = bat;
    }

    public IState HandleInput()
    {
        if (_bat.Detection.IsPlayerDetected) { return _bat.FlyState; }

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
