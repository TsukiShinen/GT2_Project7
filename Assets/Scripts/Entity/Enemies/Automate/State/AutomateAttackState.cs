using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomateAttackState : IState
{
    private AutomateController _automate;

    private bool _isAttacking;

    public AutomateAttackState(AutomateController automate)
    {
        _automate = automate;
    }

    public IState HandleInput()
    {
        if (!_isAttacking)
            return _automate.TargetState;
        else if (DayNightManager.Instance.IsDay) { return _automate.NoneState; }

        return this;
    }

    public void Update()
    {
        if (Mathf.Sign((_automate.Detection.PlayerPosition - _automate.transform.position).x) != Mathf.Sign(_automate.transform.GetChild(0).localScale.x) && _automate.Rigidbody.velocity.x != 0)
        {
            _automate.Flip();
        }
    }

    public void FixedUpdate()
    {

    }

    public void Enter()
    {
        _isAttacking = true;
        _automate.StartCoroutine(Attack());
    }

    public void Exit()
    {

    }

    private IEnumerator Attack()
    {
        _automate.AttackBox.SetActive(true);
        _automate.Animator.SetTrigger("Charge");
        yield return new WaitForSeconds(0.6f);
        _automate.AttackBox.SetActive(false);
        _isAttacking = false;
    }
}
