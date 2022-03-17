using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarJumpState : IState
{
    JarController _jar;

    public JarJumpState(JarController jar)
    {
        _jar = jar;
    }

    public IState HandleInput()
    {
        if (_jar.Rigidbody.velocity.y < 0f) { return _jar.FallState; }
        if (Input.GetButtonUp("Jump"))
        {
            OnJumpOn();
        }

        return this;
    }

    void OnJumpOn()
    {
        _jar.Rigidbody.AddForce(Vector2.down * _jar.Rigidbody.velocity.y * (1 - _jar.JumpCutMultiplier), ForceMode2D.Impulse);
        _jar.LastJumpTime = 0;
    }

    public void Update()
    {

        // Can Move while Jumping
        _jar.RunState.Update();
    }

    public void FixedUpdate()
    {

        // Can Move while Jumping
        _jar.RunState.FixedUpdate();
    }

    public void Enter()
    {
        _jar.Rigidbody.AddForce(Vector2.up * _jar.JumpForce, ForceMode2D.Impulse);
        _jar.LastGroundedTime = 0;
        _jar.LastJumpTime = 0;
        AudioManager.Instance.Play("JarJump");
    }

    public void Exit()
    {

    }
}
