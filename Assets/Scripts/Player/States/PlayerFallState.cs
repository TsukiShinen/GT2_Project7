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

    public void HandleInput()
    {
        if (Mathf.Abs(_player.Rigidbody.velocity.y) < 0.01f) { _player.ChangeState(_player.RunState); }
        else if (_player.LastGroundedTime > 0 && _player.LastJumpTime > 0) { _player.ChangeState(_player.JumpState); }
    }

    public void Update()
    {
        // Can Move while falling
        _player.RunState.Update();
    }

    public void FixedUpdate()
    {
        JumpGravity();
        // Can Move while falling
        _player.RunState.FixedUpdate();
    }

    void JumpGravity()
    {
        if (_player.Rigidbody.velocity.y < 0)
        {
            _player.Rigidbody.gravityScale = _player.GravityScale * _player.FallGravityMultiplier;
        }
        else
        {
            _player.Rigidbody.gravityScale = _player.GravityScale;
        }
    }

    public void Enter()
    {
        _player.Animator.SetBool("isFalling", true);
    }

    public void Exit()
    {
        _player.Animator.SetBool("isFalling", false);
    }
}
