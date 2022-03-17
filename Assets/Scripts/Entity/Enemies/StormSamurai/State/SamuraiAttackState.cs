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
        /*
        if (Mathf.Sign((_samurai.Detection.PlayerPosition - _samurai.transform.position).x) != Mathf.Sign(_samurai.transform.GetChild(0).localScale.x) && _samurai.Rigidbody.velocity.x != 0)
        {
            _samurai.transform.GetChild(0).localScale = new Vector3(-_samurai.transform.GetChild(0).localScale.x, _samurai.transform.GetChild(0).localScale.y, _samurai.transform.GetChild(0).localScale.z);
        }*/
    }

    public void FixedUpdate()
    {

    }

    public void Enter()
    {
        _isAttacking = true;
        _samurai.Rigidbody.velocity = Vector3.zero;
        _samurai.StartCoroutine(Attack());
    }

    public void Exit()
    {
        
    }

    private IEnumerator Attack()
    {
        string name = Random.Range(0, 3) == 0 ? "Attack2" : "Attack1";
        _samurai.Animator.SetTrigger(name);
        if (name == "Attack1")
        {
            yield return new WaitForSeconds(0.083f);
            _samurai.FirstAttackBox.SetActive(true);
            AudioManager.Instance.Play("SamuraiHit1");
            yield return new WaitForSeconds(0.083f);
            _samurai.FirstAttackBox.SetActive(false);
            yield return new WaitForSeconds(0.166f);
            _samurai.FirstAttackBox.SetActive(true);
            AudioManager.Instance.Play("SamuraiHit1");
            yield return new WaitForSeconds(0.083f);
            _samurai.FirstAttackBox.SetActive(false);
            yield return new WaitForSeconds(1f);
        }
        else if (name == "Attack2")
        {
            _samurai.StartCoroutine(FlipAfterDash());
            yield return new WaitForSeconds(0.333f);
            _samurai.lifeBar.gameObject.SetActive(false);
            AudioManager.Instance.Play("SamuraiHit2");
            _samurai.SecondAttackBox.SetActive(true);
            yield return new WaitForSeconds(0.0833f);
            _samurai.SecondAttackBox.SetActive(false);
            yield return new WaitForSeconds(0.25f);
            yield return new WaitForSeconds(1f);
        }
        _isAttacking = false;
    }

    private IEnumerator FlipAfterDash()
    {
        yield return new WaitForSeconds(0.75f);
        _samurai.transform.position = new Vector3(_samurai.transform.position.x + _samurai.SecondAttackBox.GetComponent<BoxCollider2D>().size.x * _samurai.transform.GetChild(0).transform.localScale.x, _samurai.transform.position.y, _samurai.transform.position.z);
        _samurai.lifeBar.gameObject.SetActive(true);
    }
}
