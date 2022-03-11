using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormLordController : StateMachine
{
    public float Speed = 100f;
    public float Range;

    public DetectPlayer playerDetection;

    public StormLordTargetState TargetState { get; private set; }
    public StormLordWanderState WanderState { get; private set; }
    public StormLordAttackState AttackState { get; private set; }

    public Animator Animator { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }

    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();

        TargetState = new StormLordTargetState(this);
        WanderState = new StormLordWanderState(this);
        AttackState = new StormLordAttackState(this);
    }

    void Start()
    {
        ChangeState(WanderState);
    }
}
