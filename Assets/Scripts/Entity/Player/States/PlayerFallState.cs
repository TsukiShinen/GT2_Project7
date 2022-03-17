using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : IState
{
    Player _player;

    public PlayerFallState(Player player)
    {
        _player = player;
    }

    public IState HandleInput()
    {
        if (Mathf.Abs(_player.Rigidbody.velocity.y) < 0.01f) { return _player.RunState; }
        else if (_player.LastGroundedTime > 0 && _player.LastJumpTime > 0) { return _player.JumpState; }
        else if (Input.GetButtonDown("Dash") && _player.CanDash) { return _player.DashState; }
        else if (Input.GetButtonDown("Fire2") && _player.CanAttackDistance) { return _player.DistanceAttackState; }

        return this;
    }

    public void Update()
    {
        // Can Move while falling
        _player.RunState.Update();
    }

    public void FixedUpdate()
    {
        // Can Move while falling
        _player.RunState.FixedUpdate();
    }

    public void Enter()
    {
        _player.Rigidbody.gravityScale = _player.GravityScale * _player.FallGravityMultiplier;
        _player.Animator.SetBool("isFalling", true);
    }

    public void Exit()
    {
        _player.Rigidbody.gravityScale = _player.GravityScale;
        _player.Animator.SetBool("isFalling", false);
    }
}
