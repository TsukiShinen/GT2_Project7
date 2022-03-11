using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormLordAttackState : IState
{
    private StormLordController _stormLordController;

    private bool _isAttacking;

    public StormLordAttackState(StormLordController controller)
    {
        _stormLordController = controller;
    }

    public IState HandleInput()
    {
        if (!_isAttacking) { return _stormLordController.TargetState; }

        return this;
    }

    public void Update()
    {
        if (Mathf.Sign((_stormLordController.playerDetection.PlayerPosition - _stormLordController.transform.position).x) != Mathf.Sign(_stormLordController.transform.localScale.x) && _stormLordController.Rigidbody.velocity.x != 0)
        {
            _stormLordController.transform.localScale = new Vector3(-_stormLordController.transform.localScale.x, _stormLordController.transform.localScale.y, _stormLordController.transform.localScale.z);
        }
    }

    public void FixedUpdate()
    {

    }

    public void Enter()
    {
        _isAttacking = true;
        _stormLordController.StartCoroutine(Attack());
    }

    public IEnumerator Attack()
    {
        string name = Random.Range(0, 3) == 0 ? "Attack1" : "Attack2";
        _stormLordController.Animator.SetTrigger(name);
        yield return new WaitForSeconds(_stormLordController.Animator.GetCurrentAnimatorStateInfo(0).length * _stormLordController.Animator.GetCurrentAnimatorStateInfo(0).speed);
        _isAttacking = false;
    }

    public void Exit()
    {

    }
}
