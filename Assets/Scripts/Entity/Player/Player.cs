using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    public ParticleSystem DeathParticule;

    [Header("Jump")]
    public float JumpForce;
    [Range(0f, 1f)]
    public float JumpCutMultiplier;
    [Space(10)]
    public float JumpCoyoteTime;
    public float JumpBufferTime;
    [Space(10)]
    public float FallGravityMultiplier;

    [Header("Dash")]
    public float DashingVelocity;
    public float DashingTime;
    public float ShakeTime = 0.2f;
    public float ShakeAmplitue = 5f;
    public float ShakeFrequency = 2f;

    [Header("Attack")]
    public float CooldownAttack;
    [Space(10)]
    public GameObject AttackBox;
    public GameObject ComboBox;
    public GameObject DistanceAttackBox;
    public GameObject DistanceAttackBox2;

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

    public bool CanDash { get; set; }
    public bool CanAttackDistance { get; set; }

    #region Checkpoint
    public void Respawn(Transform checkPoint)
    {
        transform.position = checkPoint.position;
        StopAllCoroutines();
        GetComponentInChildren<SpriteRenderer>().enabled = true;
        GetComponentInChildren<BoxCollider2D>().enabled = true;
        GetComponentInChildren<Rigidbody2D>().WakeUp();
        Awake();
        Start();
    }

    public void Heal()
    {
        Life = MaxLife;
        StartCoroutine(IncreaceLifeBar(Life));
    }
    #endregion

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

        CanDash = false;
        CanAttackDistance = false;
    }

    void Start()
    {
        GravityScale = Rigidbody.gravityScale;

        AttackBox.GetComponent<HitEnemy>().damage = Attack;
        ComboBox.GetComponent<HitEnemy>().damage = Attack;
        DistanceAttackBox.GetComponent<HitEnemy>().damage = Attack;
        DistanceAttackBox2.GetComponent<HitEnemy>().damage = Attack;

        //GameManager.Instance.RegisterCheckpoint(transform);
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

    public override void Check()
    {
        if (Physics2D.OverlapBox(GroundCheckPoint.position, GroundCheckSize, 0, GroundLayer))
        {
            LastGroundedTime = JumpCoyoteTime;
            CanDash = true;
            CanAttackDistance = true;
        }
    }

    public override IEnumerator Death()
    {
        yield return StartCoroutine(base.Death());
        DeathParticule.Play();
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        GetComponentInChildren<Rigidbody2D>().Sleep();
        yield return new WaitForSeconds(1f);
        StartCoroutine(GameManager.Instance.LoadLastCheckPoint());
    }

}
