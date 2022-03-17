using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormHeadAttackState : IState
{
    private StormHeadController _stormHeadController;

    private bool _isAttacking;

    public StormHeadAttackState(StormHeadController controller)
    {
        _stormHeadController = controller;
    }

    public IState HandleInput()
    {
        if (!_isAttacking) { return _stormHeadController.TargetState; }

        return this;
    }

    public void Update()
    {
        if (Mathf.Sign((_stormHeadController.Detection.PlayerPosition - _stormHeadController.transform.position).x) != Mathf.Sign(_stormHeadController.transform.GetChild(0).localScale.x) && _stormHeadController.Rigidbody.velocity.x != 0)
        {
            _stormHeadController.transform.GetChild(0).localScale = new Vector3(-_stormHeadController.transform.GetChild(0).localScale.x, _stormHeadController.transform.GetChild(0).localScale.y, _stormHeadController.transform.GetChild(0).localScale.z);
        }
    }

    public void FixedUpdate()
    {

    }

    public void Enter()
    {
        _isAttacking = true;
        _stormHeadController.Rigidbody.velocity = Vector3.zero;
        _stormHeadController.StartCoroutine(Attack());
    }

    public IEnumerator Attack()
    {
        _stormHeadController.Animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        _stormHeadController.FirstAttackBox.SetActive(true);
        AudioManager.Instance.Play("StormHeadHit1");
        yield return new WaitForSeconds(0.33333f);
        _stormHeadController.FirstAttackBox.SetActive(false);
        _stormHeadController.SecondAttackBox.SetActive(true);
        AudioManager.Instance.Play("StormHeadHit2");
        yield return new WaitForSeconds(0.33333f);
        _stormHeadController.SecondAttackBox.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        _isAttacking = false;
    }

    public void Exit()
    {

    }
}
