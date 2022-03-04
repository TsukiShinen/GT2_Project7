using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    Player _player;

    public PlayerIdleState(Player player)
    {
        _player = player;
    }

    public void HandleInput()
    {
        if (Mathf.Abs(_player.Rigidbody.velocity.y) > 0.01f) { _player.ChangeState(_player.FallState); }
        else if (_player.LastGroundedTime > 0 && _player.LastJumpTime > 0) { _player.ChangeState(_player.JumpState); }
        else if (Input.GetAxisRaw("Horizontal") != 0) { _player.ChangeState(_player.RunState); }
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {

    }

    public void Enter()
    {
        _player.Rigidbody.velocity = new Vector2(0, 0);
    }

    public void Exit()
    {

    }
}
