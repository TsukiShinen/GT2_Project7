using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatFlyState : IState
{
    private BatController _bat;

    public BatFlyState(BatController bat)
    {
        _bat = bat;
    }
    public IState HandleInput()
    {
        if (!_bat.Detection.IsPlayerDetected) { return _bat.ToIdleState; }
        if (Vector2.Distance(_bat.Detection.PlayerPosition, _bat.transform.position) <= _bat.AttackRange) { }

        return this;
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        if (Vector2.Distance(_bat.Detection.PlayerPosition, _bat.transform.position) <= _bat.AttackRange) { _bat.Rigidbody.velocity = Vector2.zero; return; }

        Vector2 dir = (_bat.Detection.PlayerPosition - _bat.transform.position).normalized;
        _bat.Rigidbody.AddForce(dir * _bat.Speed * Time.fixedDeltaTime);
    }

    public void Enter()
    {
        _bat.Animator.SetBool("Fly", true);
    }

    public void Exit()
    {

    }
}
