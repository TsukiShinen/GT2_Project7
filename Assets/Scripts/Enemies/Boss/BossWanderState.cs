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
        if (_boss.playerDetection.IsPlayerDetected) { return _boss.TargetState; }

        return this;
    }

    public void Update()
    {
        
    }

    public void FixedUpdate()
    {

    }

    public void Enter()
    {

    }

    public void Exit()
    {

    }
}
