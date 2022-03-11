using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormHeadController : StateMachine
{
    public float Speed = 100f;
    public float Range;

    public DetectPlayer playerDetection;

    public GameObject FirstAttackBox;
    public GameObject SecondAttackBox;

    public StormHeadTargetState TargetState { get; private set; }
    public StormHeadWanderState WanderState { get; private set; }
    public StormHeadAttackState AttackState { get; private set; }

    public Animator Animator { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }

    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();

        TargetState = new StormHeadTargetState(this);
        WanderState = new StormHeadWanderState(this);
        AttackState = new StormHeadAttackState(this);
    }

    void Start()
    {
        ChangeState(WanderState);
    }
}
