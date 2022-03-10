using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTargetState : IState
{
    private BossController _boss;

    public BossTargetState(BossController boss)
    {
        _boss = boss;
    }

    public IState HandleInput()
    {
        if (!_boss.playerDetection.IsPlayerDetected) { return _boss.WanderState; } 
        else if (Vector2.Distance(_boss.playerDetection.PlayerPosition, _boss.transform.position) <= _boss.Range) { return _boss.AttackState; }

        return this;
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        _boss.Animator.SetBool("Running", Mathf.Abs(_boss.Rigidbody.velocity.x) > 0.1f);

        Vector2 dir = ((_boss.playerDetection.PlayerPosition - _boss.transform.position) * new Vector2(1, 0)).normalized;
        _boss.Rigidbody.AddForce(dir * _boss.Speed * Time.fixedDeltaTime);

        if (Mathf.Sign(_boss.Rigidbody.velocity.x) != Mathf.Sign(_boss.transform.localScale.x) && _boss.Rigidbody.velocity.x != 0)
        {
            _boss.transform.localScale = new Vector3(-_boss.transform.localScale.x, _boss.transform.localScale.y, _boss.transform.localScale.z);
        }
    }

    public void Enter()
    {

    }

    public void Exit()
    {
        _boss.Animator.SetBool("Running", false);
    }
}
