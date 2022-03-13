using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
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
    public GameObject AttackBox;
    public GameObject ComboBox;
    public GameObject DistanceAttackBox;
    public GameObject DistanceAttackBox2;

    public float LastGroundedTime { get; set; }
    public float LastJumpTime { get; set; }
    public float GravityScale { get; private set; }

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
    public bool CanAttackDistance { get; set; }

    public Checkpoint LastCheckPoint
    {
        get => lastCheckPoint;
        set => lastCheckPoint = value;
    }

    public override void Awake()
    {
        base.Awake();

        TrailRenderer = GetComponentInChildren<TrailRenderer>();

        IdleState = new PlayerIdleState(this);
        RunState = new PlayerRunState(this);
        JumpState = new PlayerJumpState(this);
        FallState = new PlayerFallState(this);
        DashState = new PlayerDashState(this);
        AttackState = new PlayerAttackState(this);
        DistanceAttackState = new PlayerDistanceAttack(this);

        Life = MaxLife;
        CanDash = false;
        CanAttackDistance = false;
    }

    void Start()
    {
        lifeBar.maxValue = MaxLife;
        lifeBar.value = Life;

        GravityScale = Rigidbody.gravityScale;

        AttackBox.GetComponent<HitEnemy>().damage += Attack;
        ComboBox.GetComponent<HitEnemy>().damage += Attack;
        DistanceAttackBox.GetComponent<HitEnemy>().damage += Attack;
        DistanceAttackBox2.GetComponent<HitEnemy>().damage += Attack;

        ChangeState(IdleState);
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKey(KeyCode.Space))
        {
            LastJumpTime = JumpBufferTime;
        }

        LastJumpTime -= Time.deltaTime;
        LastGroundedTime -= Time.deltaTime;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        // Check
        if (Physics2D.OverlapBox(GroundCheckPoint.position, GroundCheckSize, 0, GroundLayer))
        {
            LastGroundedTime = JumpCoyoteTime;
            CanDash = true;
            CanAttackDistance = true;
        }
    }

    public void Respawn()
    {
        if (lastCheckPoint != null)
            transform.position = lastCheckPoint.transform.position;
        else
            GameManager.Instance.Reload();
    }

}
