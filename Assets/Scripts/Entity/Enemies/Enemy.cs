using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public DetectPlayer Detection;

    public int Direction { get; set; }

    private bool _mustTurn;
    public bool CanMove { get; set; }
    public float TimerFlip { get; set; }

    public override void Awake()
    {
        base.Awake();

        _initialTransform = transform;
        Direction = 1;
    }

    public virtual void Start()
    {
        GameManager.Instance.RegisterEnemy(gameObject);
    }

    #region Patrol
    public void Patrol()
    {
        Animator.SetBool("Running", Mathf.Abs(Rigidbody.velocity.x) > 0.1f);
        XInput = Direction;
        if (!CanMove) { return; }
        TimerFlip -= Time.deltaTime;

        if (_mustTurn || TimerFlip <= 0f) { StartCoroutine(FlipPatrol()); }
    }
    public void FixedPatrol()
    {
        Movement();
        if (!CanMove) { return; }

        _mustTurn = !Physics2D.OverlapCircle(GroundCheckPoint.position, 0.1f, GroundLayer);
    }

    private IEnumerator FlipPatrol()
    {
        CanMove = false;
        int ToDirection = -Direction;
        Direction = 0;
        XInput = Direction;
        TimerFlip = Random.Range(4f, 8f);
        yield return new WaitForSeconds(Random.Range(0.5f, 1f));
        CanMove = true;
        Direction = ToDirection;
        XInput = Direction;
    }
    #endregion

    #region Target
    public void Target()
    {
        Animator.SetBool("Running", Mathf.Abs(Rigidbody.velocity.x) > 0.1f);

        Vector2 dir = ((Detection.PlayerPosition - transform.position) * new Vector2(1, 0)).normalized;
        XInput = dir.x;
        Movement();

        if (Mathf.Sign(Rigidbody.velocity.x) != Mathf.Sign(transform.GetChild(0).localScale.x) && Rigidbody.velocity.x != 0)
        {
            Flip();
        }
    }
    #endregion

    #region Death and Rewpawn

    private Transform _initialTransform;

    public void Respawn()
    {
        transform.position = _initialTransform.position;
        lifeBar.gameObject.SetActive(true);
        Awake();
        Start();
    }

    public override IEnumerator Death()
    {
        AudioManager.Instance.Play("EnemyDeath");
        yield return StartCoroutine(base.Death());
        lifeBar.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        GameManager.Instance.AddDeadEnemy(gameObject);
    }
    #endregion

    public override void Flip()
    {
        if (!(Mathf.Sign(XInput) != Mathf.Sign(transform.GetChild(0).localScale.x) && XInput != 0)) { return; }
        transform.GetChild(0).localScale = new Vector3(-transform.GetChild(0).localScale.x, transform.GetChild(0).localScale.y, transform.GetChild(0).localScale.z);
    }

    

}
