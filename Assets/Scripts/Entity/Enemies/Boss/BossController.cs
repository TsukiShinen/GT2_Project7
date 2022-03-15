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

    private void Start()
    {
        FirstAttackBox.GetComponent<HitPlayer>().damage += Attack;
        SecondAttackBox.GetComponent<HitPlayer>().damage += Attack;

        ChangeState(WanderState);
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
