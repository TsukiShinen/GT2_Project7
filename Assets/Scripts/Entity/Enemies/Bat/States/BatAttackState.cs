using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAttackState : IState
{
    private BatController _bat;

    private bool _attacking;

    public BatAttackState(BatController bat)
    {
        _bat = bat;
    }

    public IState HandleInput()
    {
        if (!_attacking) { return _bat.FlyState; }

        return this;
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {

    }

    public void Enter()
    {
        _attacking = true;
        _bat.Animator.SetTrigger("Attack");
        _bat.Rigidbody.velocity = Vector3.zero;
        _bat.StartCoroutine(Attack());
    }

    public void Exit()
    {
        _bat.AttackTimer = _bat.AttackCooldown;
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.3f);
        _bat.AttackBox.SetActive(true);
        AudioManager.Instance.Play("BatHit");
        yield return new WaitForSeconds(0.3f);
        _bat.AttackBox.SetActive(false);
        _attacking = false;
    }
}
