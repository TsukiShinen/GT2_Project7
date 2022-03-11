using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormLordWanderState : IState
{
    private StormLordController _stormLordController;

    public StormLordWanderState(StormLordController controller)
    {
        _stormLordController = controller;
    }

    public IState HandleInput()
    {
        if (_stormLordController.playerDetection.IsPlayerDetected) { return _stormLordController.TargetState; }

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
