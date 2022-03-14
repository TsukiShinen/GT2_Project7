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

    public Transform GroundCheckPos;
    public LayerMask GroundCheckMask;
    public BoxCollider2D MyCollider { get; private set; }

    public BossAttackState AttackState { get;private set; }
    public BossTargetState TargetState { get; private set; }
    public BossWanderState WanderState { get; private set; }

    public override void Awake()
    {
        base.Awake();
        MyCollider = GetComponent<BoxCollider2D>();

        AttackState = new BossAttackState(this);
        TargetState = new BossTargetState(this);
        WanderState = new BossWanderState(this);
    }

    private void Start()
    {
        FirstAttackBox.GetComponent<HitPlayer>().damage += Attack;
        SecondAttackBox.GetComponent<HitPlayer>().damage += Attack;

        ChangeState(WanderState);
    }
}
