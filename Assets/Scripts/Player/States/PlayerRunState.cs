using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : IState
{
    Player _player;

    private float _xInput = 0;
    private int _facingDirection = 1;

    public PlayerRunState(Player player)
    {
        _player = player;
    }

    public IState HandleInput()
    {
        if (Mathf.Abs(_player.Rigidbody.velocity.y) > 0.01f) { return _player.FallState; }
        else if (Mathf.Abs(_player.Rigidbody.velocity.x) < 0.1f) { return _player.IdleState; }
        else if (_player.LastGroundedTime > 0 && _player.LastJumpTime > 0) { return _player.JumpState; }
        else if (Input.GetKeyDown(KeyCode.A) && _player.CanDash) { return _player.DashState; }

        return this;
    }

    public void Update()
    {
        _xInput = Input.GetAxis("Horizontal");
    }

    public void FixedUpdate()
    {
        Movement();
        Friction();
    }

    void Movement()
    {
        float targetSpeed = _xInput * _player.MoveSpeed;
        float speedDif = targetSpeed - _player.Rigidbody.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _player.Acceleration : _player.Decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, _player.VelPower) * Mathf.Sign(speedDif);

        _player.Rigidbody.AddForce(movement * Vector2.right);

        if (_xInput > 0.01f && _facingDirection == -1)
        {
            _facingDirection = 1;
            _player.transform.localScale = new Vector3(-_player.transform.localScale.x, _player.transform.localScale.y, _player.transform.localScale.z);
        }
        else if (_xInput < -0.01f && _facingDirection == 1)
        {
            _facingDirection = -1;
            _player.transform.localScale = new Vector3(-_player.transform.localScale.x, _player.transform.localScale.y, _player.transform.localScale.z);
        }
    }

    void Friction()
    {
        if (_player.LastGroundedTime > 0 && Mathf.Abs(_xInput) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(_player.Rigidbody.velocity.x), Mathf.Abs(_player.FrictionAmount));
            amount *= Mathf.Sign(_player.Rigidbody.velocity.x);
            _player.Rigidbody.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }

    public void Enter()
    {
        _player.Animator.SetBool("isRunning", true);
    }

    public void Exit()
    {
        _player.Animator.SetBool("isRunning", false);
    }
}
