using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiTargetState : IState
{
    private SamuraiController _samurai;

    public SamuraiTargetState(SamuraiController samurai)
    {
        _samurai = samurai;
    }

    public IState HandleInput()
    {
        if (!_samurai.Detection.IsPlayerDetected) { return _samurai.WanderState; }
        else if (Vector2.Distance(_samurai.Detection.PlayerPosition, _samurai.transform.position) <= _samurai.Range) { return _samurai.AttackState; }

        return this;
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        _samurai.Animator.SetBool("Run", Mathf.Abs(_samurai.Rigidbody.velocity.x) > 0.1f);

        Vector2 dir = ((_samurai.Detection.PlayerPosition - _samurai.transform.position) * new Vector2(1, 0)).normalized;
        _samurai.Rigidbody.AddForce(dir * _samurai.Speed * Time.fixedDeltaTime);

        if (Mathf.Sign(_samurai.Rigidbody.velocity.x) != Mathf.Sign(_samurai.transform.GetChild(0).localScale.x) && _samurai.Rigidbody.velocity.x != 0)
        {
            _samurai.transform.GetChild(0).localScale = new Vector3(-_samurai.transform.GetChild(0).localScale.x, _samurai.transform.GetChild(0).localScale.y, _samurai.transform.GetChild(0).localScale.z);
        }
    }

    public void Enter()
    {

    }

    public void Exit()
    {
        _samurai.Animator.SetBool("Run", false);
    }
}
