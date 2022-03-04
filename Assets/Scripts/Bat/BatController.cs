using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : StateMachine
{
    public DetectPlayer Detection;

    public BatIdleState IdleState { get; private set; }
    public BatFlyState FlyState { get; private set; }

    public Animator Animator { get; private set; }

    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();

        IdleState = new BatIdleState(this);
        FlyState = new BatFlyState(this);
    }

    void Start()
    {
        _currentState = IdleState;
    }
}
