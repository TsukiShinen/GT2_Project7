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
        if (!_stormHeadController.playerDetection.IsPlayerDetected) { return _stormHeadController.WanderState; }
        else if (Vector2.Distance(_stormHeadController.playerDetection.PlayerPosition, _stormHeadController.transform.position) <= _stormHeadController.Range) { return _stormHeadController.AttackState; }

        return this;
    }

    public void Enter()
    {
        Debug.Log("Target");
    }

    public void Exit()
    {
        _stormHeadController.Animator.SetBool("Running", false);
    }

    public void FixedUpdate()
    {
        _stormHeadController.Animator.SetBool("Running", Mathf.Abs(_stormHeadController.Rigidbody.velocity.x) > 0.1f);

        Vector2 dir = ((_stormHeadController.playerDetection.PlayerPosition - _stormHeadController.transform.position) * new Vector2(1, 0)).normalized;
        _stormHeadController.Rigidbody.AddForce(dir * _stormHeadController.Speed * Time.fixedDeltaTime);

        if (Mathf.Sign(_stormHeadController.Rigidbody.velocity.x) != Mathf.Sign(_stormHeadController.transform.localScale.x) && _stormHeadController.Rigidbody.velocity.x != 0)
        {
            _stormHeadController.transform.localScale = new Vector3(-_stormHeadController.transform.localScale.x, _stormHeadController.transform.localScale.y, _stormHeadController.transform.localScale.z);
        }
    }

    public void Update()
    {

    }
}
