using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomateRunState : IState
{
    private AutomateController _automate;

    public AutomateRunState(AutomateController automate)
    {
        _automate = automate;
    }

    public IState HandleInput()
    {
        if (!_automate.Detection.IsPlayerDetected)
            return _automate.ToSleepState;
        else if (Vector2.Distance(_automate.Detection.PlayerPosition, _automate.transform.position) <= _automate.AttackRange && _automate.AttackTimer <= 0)
            return _automate.AttackState;
        else if (DayNightManager.Instance.IsDay)
            return _automate.NoneState;

        return this;
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        if (Vector2.Distance(_automate.Detection.PlayerPosition, _automate.transform.position) <= _automate.AttackRange)
        {
            _automate.Rigidbody.velocity = Vector2.zero;
            _automate.Animator.SetBool("Move", false);
            return;
        }
        Vector2 dir = new Vector2(_automate.Detection.PlayerPosition.x - _automate.transform.position.x, 0).normalized;
        _automate.transform.localScale = dir == new Vector2(-1, 0) ? new Vector3(-1, _automate.transform.localScale.y, _automate.transform.localScale.z) : new Vector3(1, _automate.transform.localScale.y, _automate.transform.localScale.z);
        _automate.Rigidbody.AddForce(dir * _automate.Speed * Time.deltaTime);
        _automate.Animator.SetBool("Move", true);
    }

    public void Enter()
    {
        _automate.Animator.SetBool("Move", true);
    }

    public void Exit()
    {

    }
}
