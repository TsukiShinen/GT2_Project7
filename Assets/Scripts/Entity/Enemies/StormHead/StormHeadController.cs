using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormHeadController : Entity
{
    public float Speed = 100f;
    public float Range;

    public DetectPlayer playerDetection;

    public GameObject FirstAttackBox;
    public GameObject SecondAttackBox;

    public StormHeadTargetState TargetState { get; private set; }
    public StormHeadWanderState WanderState { get; private set; }
    public StormHeadAttackState AttackState { get; private set; }

    public override void Awake()
    {
        base.Awake();

        TargetState = new StormHeadTargetState(this);
        WanderState = new StormHeadWanderState(this);
        AttackState = new StormHeadAttackState(this);
    }

    void Start()
    {
        FirstAttackBox.GetComponent<HitPlayer>().damage += Attack;
        SecondAttackBox.GetComponent<HitPlayer>().damage += Attack;

        ChangeState(WanderState);
    }
}