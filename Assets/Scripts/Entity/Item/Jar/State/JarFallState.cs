using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarFallState : IState
{
    JarController _jar;

    public JarFallState(JarController jar)
    {
        _jar = jar;
    }

    public IState HandleInput()
    {
        if (Mathf.Abs(_jar.Rigidbody.velocity.y) < 0.01f) { return _jar.RunState; }
        else if (_jar.LastGroundedTime > 0 && _jar.LastJumpTime > 0) { return _jar.JumpState; }

        return this;
    }

    public void Update()
    {
        // Can Move while falling
        _jar.RunState.Update();
    }

    public void FixedUpdate()
    {
        JumpGravity();
        // Can Move while falling
        _jar.RunState.FixedUpdate();
    }

    void JumpGravity()
    {
        if (_jar.Rigidbody.velocity.y < 0)
        {
            _jar.Rigidbody.gravityScale = _jar.GravityScale * _jar.FallGravityMultiplier;
        }
        else
        {
            _jar.Rigidbody.gravityScale = _jar.GravityScale;
        }
    }

    public void Enter()
    {

    }

    public void Exit()
    {

    }
}
