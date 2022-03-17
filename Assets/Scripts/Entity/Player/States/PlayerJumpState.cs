using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : IState
{
    Player _player;

    public PlayerJumpState(Player player)
    {
        _player = player;
    }

    public IState HandleInput()
    {
        if (_player.Rigidbody.velocity.y < 0f) { return _player.FallState; }
        else if (Input.GetButtonUp("Jump"))
        {
            OnJumpOn();
        }
        else if (Input.GetButtonDown("Dash") && _player.CanDash) { return _player.DashState; }
        else if (Input.GetButtonDown("Fire2") && _player.CanAttackDistance) { return _player.DistanceAttackState; }

        return this;
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

        AudioManager.Instance.Play("Jump");
    }

    public void Exit()
    {
        _player.Animator.SetBool("isJumping", false);
    }
}
