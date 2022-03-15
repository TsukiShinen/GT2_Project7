using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarRunState : IState
{
    private JarController _jar;

    private float _xInput = 0;
    private int _facingDirection = 1;
    
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
        _xInput = Input.GetAxis("Horizontal");
    }

    public void FixedUpdate()
    {
        Movement();
        Friction();
    }

    void Movement()
    {
        float targetSpeed = _xInput * _jar.MoveSpeed;
        float speedDif = targetSpeed - _jar.Rigidbody.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _jar.Acceleration : _jar.Decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, _jar.VelPower) * Mathf.Sign(speedDif);

        _jar.Rigidbody.AddForce(movement * Vector2.right);

        if (_xInput > 0.01f && _facingDirection == -1)
        {
            _facingDirection = 1;
            _jar.transform.localScale = new Vector3(-_jar.transform.localScale.x, _jar.transform.localScale.y, _jar.transform.localScale.z);
            _jar.transform.GetChild(3).transform.localScale = new Vector3(1, _jar.transform.GetChild(3).transform.localScale.y, _jar.transform.GetChild(3).transform.localScale.z);
        }
        else if (_xInput < -0.01f && _facingDirection == 1)
        {
            _facingDirection = -1;
            _jar.transform.localScale = new Vector3(-_jar.transform.localScale.x, _jar.transform.localScale.y, _jar.transform.localScale.z);
            _jar.transform.GetChild(3).transform.localScale = new Vector3(-1, _jar.transform.GetChild(3).transform.localScale.y, _jar.transform.GetChild(3).transform.localScale.z);
        }

    }

    void Friction()
    {
        if (_jar.LastGroundedTime > 0 && Mathf.Abs(_xInput) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(_jar.Rigidbody.velocity.x), Mathf.Abs(_jar.FrictionAmount));
            amount *= Mathf.Sign(_jar.Rigidbody.velocity.x);
            _jar.Rigidbody.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }

    public void Enter()
    {
        _jar.Animator.SetBool("Run", true);
    }

    public void Exit()
    {
        _jar.Animator.SetBool("Run", false);
    }
}
