using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiAttackState : IState
{
    private SamuraiController _samurai;

    private bool _isAttacking;

    public SamuraiAttackState(SamuraiController samurai)
    {
        _samurai = samurai;
    }

    public IState HandleInput()
    {
        if (!_isAttacking)
            return _samurai.TargetState;

        return this;
    }

    public void Update()
    {
        if (Mathf.Sign((_samurai.Detection.PlayerPosition - _samurai.transform.position).x) != Mathf.Sign(_samurai.transform.GetChild(0).localScale.x) && _samurai.Rigidbody.velocity.x != 0)
        {
            _samurai.transform.GetChild(0).localScale = new Vector3(-_samurai.transform.GetChild(0).localScale.x, _samurai.transform.GetChild(0).localScale.y, _samurai.transform.GetChild(0).localScale.z);
        }
    }

    public void FixedUpdate()
    {

    }

    public void Enter()
    {
        _isAttacking = true;
        _samurai.StartCoroutine(Attack());
    }

    public void Exit()
    {
        
    }

    private IEnumerator Attack()
    {
        string name = Random.Range(0, 2) == 0 ? "Attack1" : "Attack2";
        GameObject attack = name == "Attack1" ? _samurai.FirstAttackBox : _samurai.SecondAttackBox;
        yield return new WaitForSeconds(0.3f);
        attack.SetActive(true);
        _samurai.Animator.SetTrigger(name);
        yield return new WaitForSeconds(0.6f);
        attack.SetActive(false);
        _isAttacking = false;
        if (name == "Attack2" && _samurai.transform.GetChild(0).localScale.x == 1)
            _samurai.transform.position = new Vector3(_samurai.transform.position.x + _samurai.SpriteRenderer.size.x, _samurai.transform.position.y, _samurai.transform.position.z);
        else if (name == "Attack2" && _samurai.transform.GetChild(0).localScale.x == -1)
            _samurai.transform.position = new Vector3(_samurai.transform.position.x - _samurai.SpriteRenderer.size.x, _samurai.transform.position.y, _samurai.transform.position.z);
    }
}
