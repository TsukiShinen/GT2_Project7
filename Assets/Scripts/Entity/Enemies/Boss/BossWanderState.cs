using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWanderState : IState
{
    private BossController _boss;

    public BossWanderState(BossController boss)
    {
        _boss = boss;
    }

    public IState HandleInput()
    {
        if (_boss.Detection.IsPlayerDetected) { return _boss.TargetState; }

        return this;
    }

    public void Update()
    {
        _boss.Patrol();
    }

    public void FixedUpdate()
    {
        _boss.FixedPatrol();
    }

    public void Enter()
    {
        _boss.CanMove = true;
        _boss.TimerFlip = Random.Range(4f, 8f);
    }

    public void Exit()
    {

    }
}
