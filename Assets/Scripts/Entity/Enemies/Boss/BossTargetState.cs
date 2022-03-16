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
        if (!_boss.Detection.IsPlayerDetected) { return _boss.WanderState; } 
        else if (Vector2.Distance(_boss.Detection.PlayerPosition, _boss.transform.position) <= _boss.Range) { return _boss.AttackState; }

        return this;
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        _boss.Target();
    }

    public void Enter()
    {

    }

    public void Exit()
    {
        _boss.Animator.SetBool("Running", false);
    }
}
