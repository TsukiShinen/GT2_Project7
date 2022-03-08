using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomateController : StateMachine
{
    public bool isDay;

    public float Speed;
    public float AttackRange;
    public float AttackCooldown = 1f;
    public float AttackTimer { get; set; }

    public DetectPlayer Detection;

    public AutomateSleepState SleepState { get; private set; }
    public AutomateAwakeState AwakeState { get; private set; }
    public AutomateRunState RunState { get; private set; }
    public AutomateToSleepState ToSleepState { get; private set; }
    public AutomateAttackState AttackState { get; private set; }
    public AutomateNoneState NoneState { get; private set; }

    public Vector3 BasePosition { get; private set; }

    public SpriteRenderer SpriteRenderer { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Animator { get; private set; }

    public void Awake()
    {
        SpriteRenderer = transform.GetChild(0).GetComponentInChildren<SpriteRenderer>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();

        BasePosition = transform.position;

        SleepState = new AutomateSleepState(this);
        AwakeState = new AutomateAwakeState(this);
        RunState = new AutomateRunState(this);
        ToSleepState = new AutomateToSleepState(this);
        AttackState = new AutomateAttackState(this);
        NoneState = new AutomateNoneState(this);
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
        _currentState = SleepState;
    }

    /*void Update()
    {
        isDay = DayNightManager.Instance.IsDay;
        _currentState = isDay ? SleepState : NoneState;
    }*/
}
