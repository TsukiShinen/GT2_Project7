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
        if (_stormLordController.Detection.IsPlayerDetected) { return _stormLordController.TargetState; }

        return this;
    }

    public void Enter()
    {
        _stormLordController.CanMove = true;
        _stormLordController.TimerFlip = Random.Range(4f, 8f);
    }

    public void Exit()
    {

    }

    public void FixedUpdate()
    {
        _stormLordController.FixedPatrol();
    }

    public void Update()
    {
        _stormLordController.Patrol();
    }
}
