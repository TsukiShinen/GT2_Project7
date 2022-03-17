using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiController : Enemy
{
    public float Range;

    public GameObject FirstAttackBox;
    public GameObject SecondAttackBox;

    public SamuraiAttackState AttackState { get; private set; }
    public SamuraiTargetState TargetState { get; private set; }
    public SamuraiWanderState WanderState { get; private set; }

    public SpriteRenderer SpriteRenderer { get; set; }

    public override void Awake()
    {
        base.Awake();

        AttackState = new SamuraiAttackState(this);
        TargetState = new SamuraiTargetState(this);
        WanderState = new SamuraiWanderState(this);
        
        SpriteRenderer = transform.GetChild(0).GetComponentInChildren<SpriteRenderer>();
    }

    public override void Start()
    {
        base.Start();

        FirstAttackBox.GetComponent<HitPlayer>().damage = Attack;
        SecondAttackBox.GetComponent<HitPlayer>().damage = Attack;

        ChangeState(WanderState);

        DayNightManager.Instance.SetLightIntensity += UpdateMaterial;
    }

    private void UpdateMaterial(float pr)
    {
        SpriteRenderer.material.SetFloat("_Intensity", 2 - pr * 2);
    }

    public override void Hit(Vector2 knockBack, int damage)
    {
        base.Hit(knockBack, damage);
        AudioManager.Instance.Play("EnemyHurt");
    }

    public override IEnumerator Death()
    {
        DayNightManager.Instance.SetLightIntensity -= UpdateMaterial;
        yield return StartCoroutine(base.Death());
    }
}
