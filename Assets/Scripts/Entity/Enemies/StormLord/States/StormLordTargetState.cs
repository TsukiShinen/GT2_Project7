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
        if (!_stormLordController.playerDetection.IsPlayerDetected) { return _stormLordController.WanderState; }
        else if (Vector2.Distance(_stormLordController.playerDetection.PlayerPosition, _stormLordController.transform.position) <= _stormLordController.Range) { return _stormLordController.AttackState; }

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
        _stormLordController.Animator.SetBool("Running", Mathf.Abs(_stormLordController.Rigidbody.velocity.x) > 0.1f);

        Vector2 dir = ((_stormLordController.playerDetection.PlayerPosition - _stormLordController.transform.position) * new Vector2(1, 0)).normalized;
        _stormLordController.Rigidbody.AddForce(dir * _stormLordController.Speed * Time.fixedDeltaTime);

        if (Mathf.Sign(_stormLordController.Rigidbody.velocity.x) != Mathf.Sign(_stormLordController.transform.GetChild(0).localScale.x) && _stormLordController.Rigidbody.velocity.x != 0)
        {
            _stormLordController.transform.GetChild(0).localScale = new Vector3(-_stormLordController.transform.GetChild(0).localScale.x, _stormLordController.transform.GetChild(0).localScale.y, _stormLordController.transform.GetChild(0).localScale.z);
        }
    }

    public void Update()
    {

    }
}
