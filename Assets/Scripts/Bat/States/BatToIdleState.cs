using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatToIdleState : IState
{
    private BatController _bat;

    public BatToIdleState(BatController bat)
    {
        _bat = bat;
    }

    public IState HandleInput()
    {
        if (_bat.transform.position == _bat.BasePosition) { return _bat.IdleState; }
        if (_bat.Detection.IsPlayerDetected) { return _bat.FlyState; }

        return this;
    }

    public void Update()
    {

    }

    public void FixedUpdate()
        
{
        if (Vector2.Distance(_bat.BasePosition, _bat.transform.position) <= 0.1f)
        {
            _bat.Rigidbody.velocity = Vector2.zero; 
            _bat.transform.position = _bat.BasePosition;
            return;
        }

        Vector2 dir = (_bat.BasePosition - _bat.transform.position).normalized;
        _bat.Rigidbody.AddForce(dir * _bat.Speed * Time.fixedDeltaTime);
    }

    public void Enter()
    {

    }

    public void Exit()
    {
        _bat.Animator.SetBool("Fly", false);
    }
}
