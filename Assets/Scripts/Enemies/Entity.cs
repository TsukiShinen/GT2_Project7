using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : StateMachine
{
    [Header("Life")]
    public int MaxLife;
    public Slider lifeBar;

    public int life { get; set; }

    public Rigidbody2D Rigidbody { get; set; }
    public Animator Animator { get; set; }

    public virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        if (Rigidbody == null) { Debug.LogWarning("RIGIDBODY MISSING ON : " + gameObject.name); }
        Animator = GetComponentInChildren<Animator>();
    }

    public void Hit(Vector2 knockBack)
    {
        life -= 1;
        if (lifeBar != null) { lifeBar.value = life; }
        StartCoroutine(GetHit(knockBack));
    }

    private IEnumerator GetHit(Vector2 knockBack)
    {
        Rigidbody.AddForce(knockBack);
        yield return new WaitForSeconds(0.5f);
        Rigidbody.velocity = new Vector2(0f, Rigidbody.velocity.y);
    }
}
