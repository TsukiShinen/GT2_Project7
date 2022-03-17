using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarRunState : IState
{
    private JarController _jar;
    
    public JarRunState(JarController jar)
    {
        _jar = jar;
    }

    public IState HandleInput()
    {
        if (Mathf.Abs(_jar.Rigidbody.velocity.x) < 0.1f) { return _jar.IdleState; }
        else if (_jar.LastGroundedTime > 0 && _jar.LastJumpTime > 0) { return _jar.JumpState; }

        return this;
    }

    public void Update()
    {
        _jar.XInput = Input.GetAxis("Horizontal");
    }

    public void FixedUpdate()
    {
        _jar._canvas.transform.parent = null;
        _jar.Movement();
        _jar._canvas.transform.parent = _jar.transform;
    }

    public void Enter()
    {
        _jar.Animator.SetBool("Run", true);
        AudioManager.Instance.Play("JarFootStep");
    }

    public void Exit()
    {
        _jar.Animator.SetBool("Run", false);
        AudioManager.Instance.Stop("JarFootStep");
    }
}
