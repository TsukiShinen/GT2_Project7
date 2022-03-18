using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarController : Entity
{
    [Header("Jump")]
    public float JumpForce;
    [Range(0f, 1f)]
    public float JumpCutMultiplier;
    [Space(10)]
    public float JumpCoyoteTime;
    public float JumpBufferTime;
    [Space(10)]
    public float FallGravityMultiplier;

    public float LastJumpTime { get; set; }
    public float GravityScale { get; private set; }

    public JarIdleState IdleState { get; private set; }
    public JarRunState RunState { get; private set; }
    public JarJumpState JumpState { get; private set; }
    public JarFallState FallState { get; private set; }

    public BoxCollider2D BoxCollider { get; private set; }

    private GameObject _player;

    public GameObject Canvas;

    private bool _isControl;

    public override void Awake()
    {
        base.Awake();

        IdleState = new JarIdleState(this);
        RunState = new JarRunState(this);
        JumpState = new JarJumpState(this);
        FallState = new JarFallState(this);

        BoxCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            _isControl = false;
            Debug.Log(_isControl);
            lifeBar = null;
            _player.transform.parent = null;
            _player.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.gameObject.tag = "Untagged";
            Animator.SetBool("Run", false);
            Animator.SetBool("Control", false);
            _currentState = null;
        }
    }

    public override void Check()
    {
        if (Physics2D.OverlapBox(GroundCheckPoint.position, GroundCheckSize, 0, GroundLayer))
        {
            LastGroundedTime = JumpCoyoteTime;
        }
        if (Physics2D.OverlapBox(GroundCheckPoint.position, GroundCheckSize, 0, GroundLayer) && !_isControl)
        {
            Debug.Log(_isControl);
            BoxCollider.isTrigger = true;
            Rigidbody.gravityScale = 0;
            Rigidbody.velocity = new Vector2(0, 0);
        }
    }

    public void PlayerControlJar()
    {
        _isControl = true;
        lifeBar = _player.gameObject.GetComponent<Entity>().lifeBar;
        _player.transform.parent = transform;
        _player.SetActive(false);
        BoxCollider.isTrigger = false;
        Rigidbody.gravityScale = 1;
        GravityScale = Rigidbody.gravityScale;
        transform.GetChild(2).gameObject.SetActive(true);
        transform.gameObject.tag = "Jar";
        Animator.SetBool("Control", true);
        ChangeState(IdleState);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) { return; }

        _player = collision.gameObject;

        Canvas = transform.GetChild(3).gameObject;
    }

}
