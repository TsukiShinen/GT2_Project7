using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : Entity
{
    public float Speed = 100f;
    public float Range;

    public DetectPlayer playerDetection;

    public GameObject FirstAttackBox;
    public GameObject SecondAttackBox;

    public BossAttackState AttackState { get;private set; }
    public BossTargetState TargetState { get; private set; }
    public BossWanderState WanderState { get; private set; }

    public override void Awake()
    {
        base.Awake();
        Animator = GetComponentInChildren<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();

        AttackState = new BossAttackState(this);
        TargetState = new BossTargetState(this);
        WanderState = new BossWanderState(this);

        life = MaxLife;
    }

    private void Start()
    {
        lifeBar.maxValue = MaxLife;
        lifeBar.value = life;
        ChangeState(WanderState);
    }
}
