using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : Enemy
{
    public float Range;

    public GameObject FirstAttackBox;
    public GameObject SecondAttackBox;

    public BossAttackState AttackState { get;private set; }
    public BossTargetState TargetState { get; private set; }
    public BossWanderState WanderState { get; private set; }

    public override void Awake()
    {
        base.Awake();

        AttackState = new BossAttackState(this);
        TargetState = new BossTargetState(this);
        WanderState = new BossWanderState(this);
    }

    public override void Start()
    {
        base.Start();

        FirstAttackBox.GetComponent<HitPlayer>().damage = Attack;
        SecondAttackBox.GetComponent<HitPlayer>().damage = Attack;

        ChangeState(WanderState);
    }

    public override void Hit(Vector2 knockBack, int damage)
    {
        base.Hit(knockBack, damage);
        AudioManager.Instance.Play("EnemyHurt");
    }
}
