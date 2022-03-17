using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : Entity
{
    public float Speed;
    public float AttackRange;
    public float AttackCooldown = 1f;
    public float AttackTimer { get; set; }

    public DetectPlayer Detection { get; private set; }
    public DetectPlayer DetectionDay;
    public DetectPlayer DetectionNight;
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
        Detection = DetectionDay;
        AttackBox.GetComponent<HitPlayer>().damage = Attack;
    }

    private void OnDay(bool IsDay)
    {
        Detection = IsDay ? DetectionDay : DetectionNight;
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
        DayNightManager.Instance.EventDay += OnDay;
        ChangeState(IdleState);
    }

    public override void Update()
    {
        base.Update();
        lifeBar.gameObject.SetActive(_currentState == IdleState ? false : true);
    }

    public override void Hit(Vector2 knockBack, int damage)
    {
        base.Hit(knockBack, damage);
        AudioManager.Instance.Play("BatHurt");
        if (!IsAlive) { StartCoroutine(Death()); }
    }

    private IEnumerator Death()
    {
        _currentState = null;
        AudioManager.Instance.Play("BatDeath");
        Rigidbody.velocity = Vector3.zero;
        lifeBar.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
