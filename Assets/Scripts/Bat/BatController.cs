using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : StateMachine
{
    public float Speed;
    public float AttackRange;
    public float AttackCooldown = 1f;
    public float AttackTimer { get; set; }

    public DetectPlayer Detection;
    public GameObject AttackBox;

    public BatIdleState IdleState { get; private set; }
    public BatFlyState FlyState { get; private set; }
    public BatToIdleState ToIdleState { get; private set; }
    public BatAttackState AttackState { get; private set; }

    public Vector3 BasePosition { get; private set; }

    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Animator { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponentInChildren<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();

        BasePosition = transform.position;

        IdleState = new BatIdleState(this);
        FlyState = new BatFlyState(this);
        ToIdleState = new BatToIdleState(this);
        AttackState = new BatAttackState(this);
    }

    protected override void LogicUpdate()
    {
        if (AttackTimer > 0)
        {
            AttackTimer -= Time.deltaTime;
        }
    }

    void Start()
    {
        _currentState = IdleState;
    }
}
