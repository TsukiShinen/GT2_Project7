using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : StateMachine
{
    public float Speed;
    public float AttackRange;

    public DetectPlayer Detection;

    public BatIdleState IdleState { get; private set; }
    public BatFlyState FlyState { get; private set; }
    public BatToIdleState ToIdleState { get; private set; }

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
    }

    void Start()
    {
        _currentState = IdleState;
    }
}
