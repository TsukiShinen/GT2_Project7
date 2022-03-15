using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiWanderState : IState
{
    private SamuraiController _samurai;

    private bool _mustTurn;
    private bool _canMove;
    private float _timerFlip;

    public SamuraiWanderState(SamuraiController samurai)
    {
        _samurai = samurai;
    }

    public IState HandleInput()
    {
        if (_samurai.Detection.IsPlayerDetected) { return _samurai.TargetState; }

        return this;
    }

    public void Update()
    {
        _samurai.Animator.SetBool("Run", Mathf.Abs(_samurai.Rigidbody.velocity.x) > 0.1f);
        if (!_canMove) { return; }
        _timerFlip -= Time.deltaTime;

        if (_mustTurn || _timerFlip <= 0f) { _samurai.StartCoroutine(Flip()); }
    }

    private IEnumerator Flip()
    {
        _canMove = false;
        _samurai.Speed *= -1;
        _samurai.Rigidbody.velocity = Vector3.zero;
        yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        _samurai.transform.GetChild(0).localScale = new Vector3(-_samurai.transform.GetChild(0).localScale.x, _samurai.transform.GetChild(0).localScale.y, _samurai.transform.GetChild(0).localScale.z);
        _timerFlip = Random.Range(4f, 8f);
        _canMove = true;
    }

    public void FixedUpdate()
    {
        if (!_canMove) { return; }
        _mustTurn = !Physics2D.OverlapCircle(_samurai.GroundCheckPos.position, 0.1f, _samurai.GroundCheckMask);

        _samurai.Rigidbody.AddForce(new Vector2(_samurai.Speed * Time.fixedDeltaTime, 0));
    }

    public void Enter()
    {
        if (Mathf.Sign(_samurai.Speed) != Mathf.Sign(_samurai.transform.GetChild(0).localScale.x)) { _samurai.Speed *= -1; }
        _canMove = true;
        _timerFlip = Random.Range(4f, 8f);
    }

    public void Exit()
    {

    }
}
