using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomateWanderState : IState
{
    private AutomateController _automate;

    private bool _mustTurn;
    private bool _canMove;
    private float _timerFlip;

    public AutomateWanderState(AutomateController automate)
    {
        _automate = automate;
    }

    public IState HandleInput()
    {
        if (_automate.Detection.IsPlayerDetected) { return _automate.TargetState; }
        else if (DayNightManager.Instance.IsDay) { return _automate.NoneState; }

        return this;
    }

    public void Update()
    {
        _automate.Animator.SetBool("Run", Mathf.Abs(_automate.Rigidbody.velocity.x) > 0.1f);
        if (!_canMove) { return; }
        _timerFlip -= Time.deltaTime;

        if (_mustTurn || _timerFlip <= 0f) { _automate.StartCoroutine(Flip()); }
    }

    private IEnumerator Flip()
    {
        _canMove = false;
        _automate.Speed *= -1;
        _automate.Rigidbody.velocity = Vector3.zero;
        yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        _automate.transform.GetChild(0).localScale = new Vector3(-_automate.transform.GetChild(0).localScale.x, _automate.transform.GetChild(0).localScale.y, _automate.transform.GetChild(0).localScale.z);
        _timerFlip = Random.Range(4f, 8f);
        _canMove = true;
    }

    public void FixedUpdate()
    {
        if (!_canMove) { return; }
        _mustTurn = !Physics2D.OverlapCircle(_automate.GroundCheckPos.position, 0.1f, _automate.GroundCheckMask);

        _automate.Rigidbody.AddForce(new Vector2(_automate.Speed * Time.fixedDeltaTime, 0));
    }

    public void Enter()
    {
        if (Mathf.Sign(_automate.Speed) != Mathf.Sign(_automate.transform.GetChild(0).localScale.x)) { _automate.Speed *= -1; }
        _canMove = true;
        _timerFlip = Random.Range(4f, 8f);
    }

    public void Exit()
    {

    }
}
