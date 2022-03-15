using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWanderState : IState
{
    private BossController _boss;

    private bool _mustTurn;
    private bool _canMove;
    private float _timerFlip;

    public BossWanderState(BossController boss)
    {
        _boss = boss;
    }

    public IState HandleInput()
    {
        if (_boss.playerDetection.IsPlayerDetected) { return _boss.TargetState; }

        return this;
    }

    public void Update()
    {
        _boss.Animator.SetBool("Running", Mathf.Abs(_boss.Rigidbody.velocity.x) > 0.1f);
        if (!_canMove) { return; }
        _timerFlip -= Time.deltaTime;

        if (_mustTurn || _timerFlip <= 0f) { _boss.StartCoroutine(Flip()); }
    }

    private IEnumerator Flip()
    {
        _canMove = false;
        _boss.Speed *= -1;
        _boss.Rigidbody.velocity = Vector3.zero;
        yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        _boss.transform.localScale = new Vector3(-_boss.transform.localScale.x, _boss.transform.localScale.y, _boss.transform.localScale.z);
        _timerFlip = Random.Range(4f, 8f);
        _canMove = true;
    }

    public void FixedUpdate()
    {
        if (!_canMove) { return; }
        _mustTurn = !Physics2D.OverlapCircle(_boss.GroundCheckPos.position, 0.1f, _boss.GroundCheckMask);

        _boss.Rigidbody.AddForce(new Vector2(_boss.Speed * Time.fixedDeltaTime, 0));
    }

    public void Enter()
    {
        if (Mathf.Sign(_boss.Speed) != Mathf.Sign(_boss.transform.localScale.x)) { _boss.Speed *= -1; }
        _canMove = true;
        _timerFlip = Random.Range(4f, 8f);
    }

    public void Exit()
    {

    }
}
