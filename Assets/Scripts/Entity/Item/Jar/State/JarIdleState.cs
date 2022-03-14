using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarIdleState : IState
{
    private JarController _jar;

    public JarIdleState(JarController jar)
    {
        _jar = jar;
    }

    public IState HandleInput()
    {
        if (_jar.LastGroundedTime > 0 && _jar.LastJumpTime > 0) { return _jar.JumpState; }
        else if (Input.GetAxisRaw("Horizontal") != 0) { return _jar.RunState; }

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
        _jar.Rigidbody.velocity = new Vector2(0, 0);
    }

    public void Exit()
    {

    }
}
