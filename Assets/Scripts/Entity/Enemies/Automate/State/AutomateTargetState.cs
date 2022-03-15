using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomateTargetState : IState
{
    private AutomateController _automate;

    public AutomateTargetState(AutomateController automate)
    {
        _automate = automate;
    }

    public IState HandleInput()
    {
        if (!_automate.Detection.IsPlayerDetected) { return _automate.WanderState; }
        else if (Vector2.Distance(_automate.Detection.PlayerPosition, _automate.transform.position) <= _automate.Range) { return _automate.AttackState; }
        else if (DayNightManager.Instance.IsDay) { return _automate.NoneState; }  

        return this;
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        _automate.Animator.SetBool("Run", Mathf.Abs(_automate.Rigidbody.velocity.x) > 0.1f);

        Vector2 dir = ((_automate.Detection.PlayerPosition - _automate.transform.position) * new Vector2(1, 0)).normalized;
        _automate.Rigidbody.AddForce(dir * _automate.Speed * Time.fixedDeltaTime);

        if (Mathf.Sign(_automate.Rigidbody.velocity.x) != Mathf.Sign(_automate.transform.GetChild(0).localScale.x) && _automate.Rigidbody.velocity.x != 0)
        {
            _automate.transform.GetChild(0).localScale = new Vector3(-_automate.transform.GetChild(0).localScale.x, _automate.transform.GetChild(0).localScale.y, _automate.transform.GetChild(0).localScale.z);
        }
    }

    public void Enter()
    {

    }

    public void Exit()
    {
        _automate.Animator.SetBool("Run", false);
    }
}
