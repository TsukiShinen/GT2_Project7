using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiController : StateMachine
{
    public float Speed;
    public float AttackRange;

    public DetectPlayer Detection;

    public SamuraiIdleState IdleState { get; private set; }
    public SamuraiRunState RunState { get; private set; }
    public SamuraiToIdleState ToIdleState { get; private set; }
    public SamuraiAttackState AttackState { get; private set; }

    public Vector3 BasePosition { get; private set; }

    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Animator { get; private set; }

    public void Awake()
    {
        Rigidbody = GetComponentInChildren<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();

        BasePosition = transform.position;

        IdleState = new SamuraiIdleState(this);
        RunState = new SamuraiRunState(this);
        ToIdleState = new SamuraiToIdleState(this);
        AttackState = new SamuraiAttackState(this);
    }

    void Start()
    {
        _currentState = IdleState;
    }
}
