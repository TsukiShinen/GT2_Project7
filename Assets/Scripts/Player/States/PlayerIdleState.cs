using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IState
{
    Player _player;

    public PlayerIdleState(Player player)
    {
        _player = player;
    }

    public IState HandleInput()
    {
        if (Mathf.Abs(_player.Rigidbody.velocity.y) > 0.01f) { return _player.FallState; }
        else if (_player.LastGroundedTime > 0 && _player.LastJumpTime > 0) { return _player.JumpState; }
        else if (Input.GetAxisRaw("Horizontal") != 0) { return _player.RunState; }
        else if (Input.GetButtonDown("Dash") && _player.CanDash) { return _player.DashState; }
        else if (Input.GetButtonDown("Fire1")) { return _player.AttackState; }
        else if (Input.GetButtonDown("Fire2") && _player.CanAttackDistance) { return _player.DistanceAttackState; }

        return this;
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
