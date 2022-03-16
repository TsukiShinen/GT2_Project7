using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormHeadTargetState : IState
{
    private StormHeadController _stormHeadController;

    public StormHeadTargetState (StormHeadController controller)
    {
        _stormHeadController = controller;
    }

    public IState HandleInput()
    {
        if (!_stormHeadController.Detection.IsPlayerDetected) { return _stormHeadController.WanderState; }
        else if (Vector2.Distance(_stormHeadController.Detection.PlayerPosition, _stormHeadController.transform.position) <= _stormHeadController.Range) { return _stormHeadController.AttackState; }

        return this;
    }

    public void Enter()
    {

    }

    public void Exit()
    {
        _stormHeadController.Animator.SetBool("Running", false);
    }

    public void FixedUpdate()
    {
        _stormHeadController.Target();
    }

    public void Update()
    {

    }
}
