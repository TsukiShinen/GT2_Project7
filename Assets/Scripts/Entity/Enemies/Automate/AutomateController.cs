using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomateController : Enemy
{
    public float Range;

    public GameObject AttackBox;

    public AutomateAttackState AttackState { get; private set; }
    public AutomateTargetState TargetState { get; private set; }
    public AutomateWanderState WanderState { get; private set; }
    public AutomateNoneState NoneState { get; private set; }

    public GameObject Prefab;

    public override void Awake()
    {
        base.Awake();

        AttackState = new AutomateAttackState(this);
        TargetState = new AutomateTargetState(this); 
        WanderState = new AutomateWanderState(this);
        NoneState = new AutomateNoneState(this);
    }

    void Start()
    {
        AttackBox.GetComponent<HitPlayer>().damage += Attack;

        ChangeState(WanderState);
    }
}
