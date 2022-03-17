using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : StateMachine
{
    [Header("Entity")]
    [SerializeField]
    private EntityData EntityData;
    public Slider lifeBar;
    public ParticleSystem HurtParticle;

    [Header("Check")]
    public Transform GroundCheckPoint;
    public Vector2 GroundCheckSize;
    [Space(10)]
    public LayerMask GroundLayer;

    #region Stats
    public int MaxLife { get; private set; }
    public int Attack { get; private set; }
    public int Defense { get; private set; }
    #endregion

    #region life
    public float Life { get; set; }
    public bool IsAlive { get { return Life > 0; } }
    public IEnumerator DecreaseLifeBar(float newValue)
    {
        while (lifeBar.value != newValue)
        {
            lifeBar.value -= Time.deltaTime * 4;
            if (lifeBar.value < newValue) { lifeBar.value = newValue; break; }

            yield return null;
        }
    }

    public IEnumerator IncreaceLifeBar(float newValue)
    {
        while (lifeBar.value != newValue)
        {
            lifeBar.value += Time.deltaTime * 4;
            if (lifeBar.value > newValue) { lifeBar.value = newValue; }

            yield return null;
        }
    }
    #endregion

    #region Movement
    public float MoveSpeed { get; private set; }
    public float Acceleration { get; private set; }
    public float Decceleration { get; private set; }
    public float VelPower { get; private set; }
    public float FrictionAmount { get; private set; }

    public float XInput { get; set; }
    public int FacingDirection { get; set; }
    public float LastGroundedTime { get; set; }

    public void Movement()
    {
        float targetSpeed = XInput * MoveSpeed;
        float speedDif = targetSpeed - Rigidbody.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Acceleration : Decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, VelPower) * Mathf.Sign(speedDif);
        Rigidbody.AddForce(movement * Vector2.right);

        Flip();

        Friction();
    }

    public void Friction()
    {
        if (LastGroundedTime > 0 && Mathf.Abs(XInput) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(Rigidbody.velocity.x), Mathf.Abs(FrictionAmount));
            amount *= Mathf.Sign(Rigidbody.velocity.x);
            Rigidbody.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }

    public virtual void Flip()
    {
        if (!(Mathf.Sign(XInput) != Mathf.Sign(transform.localScale.x) && XInput != 0)) { return; }
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    #endregion

    public Rigidbody2D Rigidbody { get; set; }
    public Animator Animator { get; set; }

    public virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        if (Rigidbody == null) { Debug.LogWarning("Rigidbody MISSING ON : " + gameObject.name); }
        Animator = GetComponentInChildren<Animator>();
        if (Rigidbody == null) { Debug.LogWarning("Animator MISSING ON : " + gameObject.name); }

        LoadEntityData();

        Life = MaxLife;
        if (lifeBar != null) { lifeBar.maxValue = MaxLife; lifeBar.value = Life; }

        XInput = 0;
        FacingDirection = 1;
        LastGroundedTime = 0;
    }

    private void LoadEntityData()
    {
        MaxLife = EntityData.MaxLife;
        Attack = EntityData.Attack;
        Defense = EntityData.Defense;
        MoveSpeed = EntityData.MoveSpeed;
        Acceleration = EntityData.Acceleration;
        Decceleration = EntityData.Decceleration;
        VelPower = EntityData.VelPower;
        FrictionAmount = EntityData.FrictionAmount;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        Check();
    }

    #region Check
    public virtual void Check()
    {
        LastGroundedTime = 1f;
    }
    #endregion

    #region Damage
    public void Burn(int damagePerSeconds)
    {
        Life -= damagePerSeconds * Time.deltaTime;
        if (lifeBar != null) { lifeBar.value = Life; }
    }

    public virtual void Hit(Vector2 knockBack, int damage)
    {
        Life -= damage - Defense;
        if (lifeBar != null) { StartCoroutine(DecreaseLifeBar(Life)); }

        string animToPlayer = IsAlive ? "Hit" : "Die";
        Animator.SetTrigger(animToPlayer);

        
        if (IsAlive) { StartCoroutine(GetHit(knockBack)); }
        else {
            StopAllCoroutines();
            StartCoroutine(Death()); 
        }
    }

    public virtual IEnumerator Death()
    {
        Rigidbody.velocity = Vector3.zero;
        _currentState = null;
        yield return null;
    }

    private IEnumerator GetHit(Vector2 knockBack)
    {
        Rigidbody.AddForce(knockBack);
        if (HurtParticle != null) { HurtParticle.Play(); }
        yield return new WaitForSeconds(0.5f);
        Rigidbody.velocity = new Vector2(0f, Rigidbody.velocity.y);
    }
    #endregion
}
