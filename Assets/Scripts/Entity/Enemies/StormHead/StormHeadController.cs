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

    public Transform GroundCheckPos;
    public LayerMask GroundCheckMask;

    public StormHeadTargetState TargetState { get; private set; }
    public StormHeadWanderState WanderState { get; private set; }
    public StormHeadAttackState AttackState { get; private set; }

    public override void Awake()
    {
        base.Awake();

        TargetState = new StormHeadTargetState(this);
        WanderState = new StormHeadWanderState(this);
        AttackState = new StormHeadAttackState(this);

        Life = MaxLife;
    }

    void Start()
    {
        FirstAttackBox.GetComponent<HitPlayer>().damage += Attack;
        SecondAttackBox.GetComponent<HitPlayer>().damage += Attack;

        ChangeState(WanderState);

        lifeBar.maxValue = MaxLife;
        lifeBar.value = Life;
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
