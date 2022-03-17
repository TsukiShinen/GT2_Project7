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

    public override void Start()
    {
        base.Start();

        AttackBox.GetComponent<HitPlayer>().damage = Attack;

        ChangeState(WanderState);
    }

    public override void Update()
    {
        base.Update();
        lifeBar.gameObject.SetActive(_currentState == NoneState ? false : true);
    }

    public override IEnumerator Death()
    {
        AudioManager.Instance.Play("EnemyDeath");
        Rigidbody.velocity = Vector3.zero;
        _currentState = null;
        lifeBar.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        GameManager.Instance.AddDeadEnemy(gameObject);
        Instantiate(Prefab, transform.position, Quaternion.identity);
    }

    public override void Hit(Vector2 knockBack, int damage)
    {
        if(DayNightManager.Instance.IsDay) { return; }
        base.Hit(knockBack, damage);
    }

}
