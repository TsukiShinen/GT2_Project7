using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomateController : Entity
{
    public float Speed;
    public float AttackRange;
    public float AttackCooldown = 1f;
    public float AttackTimer { get; set; }

    public DetectPlayer Detection;
    public GameObject AttackBox;

    public AutomateSleepState SleepState { get; private set; }
    public AutomateAwakeState AwakeState { get; private set; }
    public AutomateRunState RunState { get; private set; }
    public AutomateToSleepState ToSleepState { get; private set; }
    public AutomateAttackState AttackState { get; private set; }
    public AutomateNoneState NoneState { get; private set; }

    public Vector3 BasePosition { get; private set; }

    public override void Awake()
    {
        base.Awake();
        BasePosition = transform.position;

        SleepState = new AutomateSleepState(this);
        AwakeState = new AutomateAwakeState(this);
        RunState = new AutomateRunState(this);
        ToSleepState = new AutomateToSleepState(this);
        AttackState = new AutomateAttackState(this);
        NoneState = new AutomateNoneState(this);

        life = MaxLife;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (AttackTimer > 0)
        {
            AttackTimer -= Time.deltaTime;
        }
    }

    void Start()
    {
        lifeBar.maxValue = MaxLife;
        lifeBar.value = life;
        ChangeState(SleepState);
    }

    public override void Update()
    {
        base.Update();
        lifeBar.gameObject.SetActive(_currentState == SleepState || _currentState == NoneState ? false : true);
    }
}
