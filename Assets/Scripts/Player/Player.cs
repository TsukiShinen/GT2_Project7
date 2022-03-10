using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : StateMachine
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

    [Header("Check")]
    public Transform GroundCheckPoint;
    public Vector2 GroundCheckSize;
    [Space(10)]
    public LayerMask GroundLayer;

    [Header("Dash")]
    public float DashingVelocity;
    public float DashingTime;

    [Header("Attack")]
    public float CooldownAttack;
    [Space(10)]
    public BoxCollider2D AttackBox;
    public BoxCollider2D ComboBox;
    public BoxCollider2D DistanceAttackBox;
    public BoxCollider2D DistanceAttackBox2;


    [Header("Life")]
    public int MaxLife;
    public int life { get; private set; }
    public Slider lifeBar;

    public float LastGroundedTime { get; set; }
    public float LastJumpTime { get; set; }
    public float GravityScale { get; private set; }

    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public TrailRenderer TrailRenderer { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerFallState FallState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerDistanceAttack DistanceAttackState { get; private set; }

    private Checkpoint lastCheckPoint;

    public bool CanDash { get; set; }

    public Checkpoint LastCheckPoint
    {
        get => lastCheckPoint;
        set => lastCheckPoint = value;
    }


    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();
        TrailRenderer = GetComponentInChildren<TrailRenderer>();

        IdleState = new PlayerIdleState(this);
        RunState = new PlayerRunState(this);
        JumpState = new PlayerJumpState(this);
        FallState = new PlayerFallState(this);
        DashState = new PlayerDashState(this);
        AttackState = new PlayerAttackState(this);
        DistanceAttackState = new PlayerDistanceAttack(this);

        life = MaxLife;
        CanDash = false;
    }

    void Start()
    {
        lifeBar.maxValue = MaxLife;
        lifeBar.value = life;

        GravityScale = Rigidbody.gravityScale;

        _currentState = IdleState;
    }

    protected override void LogicUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            LastJumpTime = JumpBufferTime;
        }

        LastJumpTime -= Time.deltaTime;
        LastGroundedTime -= Time.deltaTime;
    }

    protected override void PhysicsUpdate()
    {
        // Check
        if (Physics2D.OverlapBox(GroundCheckPoint.position, GroundCheckSize, 0, GroundLayer))
        {
            LastGroundedTime = JumpCoyoteTime;
            CanDash = true;
        }
    }

    public void Respawn()
    {
        if (lastCheckPoint != null)
            transform.position = lastCheckPoint.transform.position;
        else
            GameManager.Instance.Reload();
    }

    public void Hit(Vector2 knockBack)
    {
        life -= 1;
        lifeBar.value = life;
        StartCoroutine(GetHit(knockBack));
    }

    private IEnumerator GetHit(Vector2 knockBack)
    {
        Rigidbody.AddForce(knockBack);
        yield return new WaitForSeconds(0.5f);
        Rigidbody.velocity = new Vector2(0f, Rigidbody.velocity.y);
    }

}
