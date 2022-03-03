using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : IPlayerState
{
    Player _player;

    public PlayerJumpState(Player player)
    {
        _player = player;
    }

    public void HandleInput()
    {
        if (_player.Rigidbody.velocity.y < 0f) { _player.ChangeState(_player.FallState); }
        else if (Input.GetButtonUp("Jump"))
        {
            OnJumpOn();
        }
    }

    void OnJumpOn()
    {
        _player.Rigidbody.AddForce(Vector2.down * _player.Rigidbody.velocity.y * (1 - _player.JumpCutMultiplier), ForceMode2D.Impulse);
        _player.LastJumpTime = 0;
    }

    public void Update()
    {

        // Can Move while Jumping
        _player.RunState.Update();
    }

    public void FixedUpdate()
    {

        // Can Move while Jumping
        _player.RunState.FixedUpdate();
    }

    public void Enter()
    {
        _player.Animator.SetBool("isJumping", true);

        _player.Rigidbody.AddForce(Vector2.up * _player.JumpForce, ForceMode2D.Impulse);
        _player.LastGroundedTime = 0;
        _player.LastJumpTime = 0;
    }

    public void Exit()
    {
        _player.Animator.SetBool("isJumping", false);
    }
}