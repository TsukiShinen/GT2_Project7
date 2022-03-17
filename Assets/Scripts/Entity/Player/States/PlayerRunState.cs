using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : IState
{
    Player _player;

    public PlayerRunState(Player player)
    {
        _player = player;
    }

    public IState HandleInput()
    {
        if (Mathf.Abs(_player.Rigidbody.velocity.y) > 0.01f) { return _player.FallState; }
        else if (Mathf.Abs(_player.Rigidbody.velocity.x) < 0.1f) { return _player.IdleState; }
        else if (_player.LastGroundedTime > 0 && _player.LastJumpTime > 0) { return _player.JumpState; }
        else if (Input.GetButtonDown("Dash") && _player.CanDash) { return _player.DashState; }
        else if (Input.GetButtonDown("Fire1")) { return _player.AttackState; }
        else if (Input.GetButtonDown("Fire2") && _player.CanAttackDistance) { return _player.DistanceAttackState; }

        return this;
    }

    public void Update()
    {
        _player.XInput = Input.GetAxis("Horizontal");
    }

    public void FixedUpdate()
    {
        _player.Movement();
    }

    public void Enter()
    {
        _player.Animator.SetBool("isRunning", true);
        AudioManager.Instance.Play("Footstep");
    }

    public void Exit()
    {
        _player.Animator.SetBool("isRunning", false);
        AudioManager.Instance.Stop("Footstep");
    }
}
