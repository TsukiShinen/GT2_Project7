using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiController : StateMachine
{
    public float Speed;
    public float AttackRange;
    public float AttackCooldown = 1f;
    public float AttackTimer { get; set; }

    public DetectPlayer Detection;
    public GameObject AttackBox;

    public SamuraiIdleState IdleState { get; private set; }
    public SamuraiRunState RunState { get; private set; }
    public SamuraiToIdleState ToIdleState { get; private set; }
    public SamuraiAttackState AttackState { get; private set; }

    public Vector3 BasePosition { get; private set; }

    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Animator { get; private set; }

    public void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();

        BasePosition = transform.position;

        IdleState = new SamuraiIdleState(this);
        RunState = new SamuraiRunState(this);
        ToIdleState = new SamuraiToIdleState(this);
        AttackState = new SamuraiAttackState(this);
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
