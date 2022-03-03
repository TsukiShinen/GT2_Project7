using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _acceleration;
    [SerializeField]
    private float _decceleration;
    [SerializeField]
    private float _velPower; 
    [Space(10)]
    [SerializeField]
    private float _frictionAmount;

    [Header("Jump")]
    [SerializeField]
    private float _jumpForce;
    [SerializeField] [Range(0f, 1f)]
    private float _jumpCutMultiplier;
    [Space(10)]
    [SerializeField]
    private float _jumpCoyoteTime;
    [SerializeField]
    private float _jumpBufferTime;
    [Space(10)]
    [SerializeField]
    private float _fallGravityMultiplier;

    [Header("Cheack")]
    [SerializeField]
    private Transform _GroundCheckPoint;
    [SerializeField]
    private Vector2 _groundCheckSize;
    [Space(10)]
    [SerializeField]
    private LayerMask _groundLayer;

    private int _facingDirection = 1;

    private float _xInput = 0;
    private float _lastGroundedTime = 0;
    private float _lastJumpTime = 0;
    private float _gravityScale;

    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private bool _isJumping = false;
    private bool _jumpInputReleased = false;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();

        _gravityScale = _rigidbody.gravityScale;
    }

    void Update()
    {
        Inputs();
        Checks();
        Anim();

        _lastJumpTime -= Time.deltaTime;
        _lastGroundedTime -= Time.deltaTime;
    }

    void Inputs()
    {
        _xInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.Space))
        {
            _lastJumpTime = _jumpBufferTime;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            OnJumpOn();
        }
    }

    void OnJumpOn()
    {
        if (_rigidbody.velocity.y > 0 && _isJumping)
        {
            _rigidbody.AddForce(Vector2.down * _rigidbody.velocity.y * (1 - _jumpCutMultiplier), ForceMode2D.Impulse);
        }

        _jumpInputReleased = true;
        _lastJumpTime = 0;
    }

    void Checks()
    {
        if (Physics2D.OverlapBox(_GroundCheckPoint.position, _groundCheckSize, 0, _groundLayer))
        {
            _lastGroundedTime = _jumpCoyoteTime;
        }

        if (_rigidbody.velocity.y < 0)
        {
            _isJumping = false;
        }
    }

    void Anim()
    {
        _animator.SetBool("isRunning", Mathf.Abs(_rigidbody.velocity.x) > 0.1f);
        _animator.SetBool("isJumping", _rigidbody.velocity.y > 0.1f);
        _animator.SetBool("isFalling", _rigidbody.velocity.y < -0.01f);
    }

    void FixedUpdate()
    {
        Movement();
        Friction();

        if (_lastGroundedTime > 0 && _lastJumpTime > 0 && !_isJumping)
        {
            Jump();
        }
        JumpGravity();
    }

    void Movement()
    {
        float targetSpeed = _xInput * _moveSpeed;
        float speedDif = targetSpeed - _rigidbody.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _acceleration : _decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, _velPower) * Mathf.Sign(speedDif);

        _rigidbody.AddForce(movement * Vector2.right);

        if (_xInput > 0.01f && _facingDirection == -1)
        {
            _facingDirection = 1;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        } else if (_xInput < -0.01f && _facingDirection == 1)
        {
            _facingDirection = -1;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    void Friction()
    {
        if (_lastGroundedTime > 0 && Mathf.Abs(_xInput) < 0.01f)
        {
            float  amount = Mathf.Min(Mathf.Abs(_rigidbody.velocity.x), Mathf.Abs(_frictionAmount));
            amount *= Mathf.Sign(_rigidbody.velocity.x);
            _rigidbody.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }

    void Jump()
    {
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        _lastGroundedTime = 0;
        _lastJumpTime = 0;
        _isJumping = true;
        _jumpInputReleased = false;
    }

    void JumpGravity()
    {
        if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.gravityScale = _gravityScale * _fallGravityMultiplier;
        } else
        {
            _rigidbody.gravityScale = _gravityScale;
        }
    }
}
