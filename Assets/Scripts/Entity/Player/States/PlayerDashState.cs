using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : IState
{
    Player _player;

    private Vector2 _dashingDir;
    private bool _isDashing = true;

    public PlayerDashState(Player player)
    {
        _player = player;
    }

    public IState HandleInput()
    {
        if (!_isDashing) { return _player.FallState; }

        return this;
    }

    public void Update()
    {
        _player.CanDash = false;
        _player.Rigidbody.velocity = _dashingDir.normalized * _player.DashingVelocity;
    }

    public void FixedUpdate()
    {

    }

    public void Enter()
    {
        AudioManager.Instance.Play("Dash");
        _player.CanDash = false;
        _isDashing = true;
        _player.TrailRenderer.emitting = true;
        _dashingDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (_dashingDir == Vector2.zero)
        {
            _dashingDir = new Vector2(_player.transform.localScale.x, 0);
        }
        _player.StartCoroutine(StopDashing());
        _player.StartCoroutine(GameManager.Instance.ShakeCamera(_player.ShakeTime, _player.ShakeAmplitue, _player.ShakeFrequency));
    }
    
    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(_player.DashingTime);
        _player.TrailRenderer.emitting = false;
        _isDashing = false;
    }

    public void Exit()
    {
        _player.Rigidbody.velocity = Vector2.zero;
        _player.Rigidbody.AddForce(_dashingDir.normalized * _player.DashingVelocity * 10);
    }
}
