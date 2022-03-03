using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float MoveSpeed;
    public float Acceleration;
    public float Decceleration;
    public float VelPower;
    [Space(10)]
    public float FrictionAmount;

    [Header("Jump")]
    public float JumpForce;
    [Range(0f, 1f)]
    public float JumpCutMultiplier;
    [Space(10)]
    public float JumpCoyoteTime;
    public float JumpBufferTime;
    [Space(10)]
    public float FallGravityMultiplier;

    [Header("Cheack")]
    public Transform GroundCheckPoint;
    public Vector2 GroundCheckSize;
    [Space(10)]
    public LayerMask GroundLayer;

    public float LastGroundedTime { get; set; }
    public float LastJumpTime { get; set; }
    public float GravityScale { get; private set; }

    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Animator { get; private set; }

    private IPlayerState _currentState;
    public PlayerIdleState IdleState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerFallState FallState { get; private set; }

    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();

        IdleState = new PlayerIdleState(this);
        RunState = new PlayerRunState(this);
        JumpState = new PlayerJumpState(this);
        FallState = new PlayerFallState(this);
    }

    void Start()
    {
        _currentState = IdleState;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            LastJumpTime = JumpBufferTime;
        }

        _currentState.HandleInput();
        _currentState.Update();


        LastJumpTime -= Time.deltaTime;
        LastGroundedTime -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        // Check
        if (Physics2D.OverlapBox(GroundCheckPoint.position, GroundCheckSize, 0, GroundLayer))
        {
            LastGroundedTime = JumpCoyoteTime;
        }

        _currentState.FixedUpdate();
    }

    public void ChangeState(IPlayerState state) 
    {
        if (_currentState != null) 
        {
            _currentState.Exit();
        }
        _currentState = state;
        Debug.Log("Player Change State : " + state.ToString());
        _currentState.Enter();
    }
}