using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : StateMachine
{
    public float Speed = 100f;
    public float Range;

    public DetectPlayer playerDetection;

    public GameObject FirstAttackBox;
    public GameObject SecondAttackBox;

    public BossAttackState AttackState { get;private set; }
    public BossTargetState TargetState { get; private set; }
    public BossWanderState WanderState { get; private set; }

    public Animator Animator { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }

    public void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();

        AttackState = new BossAttackState(this);
        TargetState = new BossTargetState(this);
        WanderState = new BossWanderState(this);
    }

    private void Start()
    {
        ChangeState(WanderState);
    }
}
