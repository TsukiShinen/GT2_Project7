using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : Entity
{
    public float Speed;
    public float AttackRange;
    public float AttackCooldown = 1f;
    public float AttackTimer { get; set; }

    public DetectPlayer Detection;
    public GameObject AttackBox;

    public BatIdleState IdleState { get; private set; }
    public BatFlyState FlyState { get; private set; }
    public BatToIdleState ToIdleState { get; private set; }
    public BatAttackState AttackState { get; private set; }

    public Vector3 BasePosition { get; private set; }

    public override void Awake()
    {
        base.Awake();
        BasePosition = transform.position;

        IdleState = new BatIdleState(this);
        FlyState = new BatFlyState(this);
        ToIdleState = new BatToIdleState(this);
        AttackState = new BatAttackState(this);

        AttackBox.GetComponent<HitPlayer>().damage = Attack;
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

    public override void Hit(Vector2 knockBack, int damage)
    {
        base.Hit(knockBack, damage);

        if (!IsAlive) { StartCoroutine(Death()); }
    }

    private IEnumerator Death()
    {
        _currentState = null;
        Rigidbody.velocity = Vector3.zero;
        lifeBar.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
