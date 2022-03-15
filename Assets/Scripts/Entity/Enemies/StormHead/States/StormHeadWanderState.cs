using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormHeadWanderState : IState
{
    private StormHeadController _stormHeadController;

    private bool _mustTurn;
    private bool _canMove;
    private float _timerFlip;

    public StormHeadWanderState(StormHeadController controller)
    {
        _stormHeadController = controller;
    }

    public IState HandleInput()
    {
        if (_stormHeadController.playerDetection.IsPlayerDetected) { return _stormHeadController.TargetState; }

        return this;
    }

    public void Enter()
    {
        if (Mathf.Sign(_stormHeadController.Speed) != Mathf.Sign(_stormHeadController.transform.GetChild(0).localScale.x)) { _stormHeadController.Speed *= -1; }
        _canMove = true;
        _timerFlip = Random.Range(4f, 8f);
    }

    public void Exit()
    {

    }

    public void FixedUpdate()
    {
        if (!_canMove) { return; }
        _mustTurn = !Physics2D.OverlapCircle(_stormHeadController.GroundCheckPos.position, 0.1f, _stormHeadController.GroundCheckMask);

        _stormHeadController.Rigidbody.AddForce(new Vector2(_stormHeadController.Speed * Time.fixedDeltaTime, 0));
    }

    public void Update()
    {
        _stormHeadController.Animator.SetBool("Running", Mathf.Abs(_stormHeadController.Rigidbody.velocity.x) > 0.1f);
        if (!_canMove) { return; }
        _timerFlip -= Time.deltaTime;

        if (_mustTurn || _timerFlip <= 0f) { _stormHeadController.StartCoroutine(Flip()); }
    }

    private IEnumerator Flip()
    {
        Debug.Log("Flip");
        _canMove = false;
        _stormHeadController.Speed *= -1;
        _stormHeadController.Rigidbody.velocity = Vector3.zero;
        yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        _stormHeadController.transform.GetChild(0).localScale = new Vector3(-_stormHeadController.transform.GetChild(0).localScale.x, _stormHeadController.transform.GetChild(0).localScale.y, _stormHeadController.transform.GetChild(0).localScale.z);
        _timerFlip = Random.Range(4f, 8f);
        _canMove = true;
    }
}
