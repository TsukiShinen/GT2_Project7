using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormLordTargetState : IState
{
    private StormLordController _stormLordController;

    public StormLordTargetState(StormLordController controller)
    {
        _stormLordController = controller;
    }

    public IState HandleInput()
    {
        if (!_stormLordController.Detection.IsPlayerDetected) { return _stormLordController.WanderState; }
        else if (Vector2.Distance(_stormLordController.Detection.PlayerPosition, _stormLordController.transform.position) <= _stormLordController.Range) { return _stormLordController.AttackState; }

        return this;
    }

    public void Enter()
    {

    }

    public void Exit()
    {
        _stormLordController.Animator.SetBool("Running", false);
    }

    public void FixedUpdate()
    {
        _stormLordController.Target();
    }

    public void Update()
    {

    }
}
