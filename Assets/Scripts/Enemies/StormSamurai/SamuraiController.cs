using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiController : Entity
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

    public override void Awake()
    {
        base.Awake();

        BasePosition = transform.position;

        IdleState = new SamuraiIdleState(this);
        RunState = new SamuraiRunState(this);
        ToIdleState = new SamuraiToIdleState(this);
        AttackState = new SamuraiAttackState(this);
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
        ChangeState(IdleState);
    }
}
