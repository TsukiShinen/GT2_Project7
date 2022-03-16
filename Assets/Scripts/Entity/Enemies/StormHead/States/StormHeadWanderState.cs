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
        if (_stormHeadController.Detection.IsPlayerDetected) { return _stormHeadController.TargetState; }

        return this;
    }

    public void Enter()
    {
        _stormHeadController.CanMove = true;
        _stormHeadController.TimerFlip = Random.Range(4f, 8f);
    }

    public void Exit()
    {

    }

    public void FixedUpdate()
    {
        _stormHeadController.FixedPatrol();
    }

    public void Update()
    {
        _stormHeadController.Patrol();
    }
}
