using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiController : Entity
{
    public float Speed;
    public float Range;

    public DetectPlayer Detection;

    public GameObject FirstAttackBox;
    public GameObject SecondAttackBox;

    public Transform GroundCheckPos;
    public LayerMask GroundCheckMask;

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

    void Start()
    {
        FirstAttackBox.GetComponent<HitPlayer>().damage += Attack;
        SecondAttackBox.GetComponent<HitPlayer>().damage += Attack;

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
