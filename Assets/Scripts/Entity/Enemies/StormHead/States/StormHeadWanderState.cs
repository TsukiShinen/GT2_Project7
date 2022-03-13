using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormHeadWanderState : IState
{
    private StormHeadController _stormHeadController;

    public StormHeadWanderState(StormHeadController controller)
    {
        _stormHeadController = controller;
    }

    public IState HandleInput()
    {
        if (_stormHeadController.playerDetection.IsPlayerDetected) { return _stormHeadController.TargetState; }
        
        return this;
    }

    public void Enter()
    {

    }

    public void Exit()
    {

    }

    public void FixedUpdate()
    {

    }

    public void Update()
    {

    }
}
