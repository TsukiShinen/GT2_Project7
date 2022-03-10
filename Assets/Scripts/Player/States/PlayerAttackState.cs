using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : IState
{
    Player _player;

    private bool _isAttacking;
    private bool _doCombo;

    public PlayerAttackState(Player player)
    {
        _player = player;
    }

    public IState HandleInput()
    {
        if (!_isAttacking) { return _player.IdleState; }
        else if (Input.GetButtonDown("Fire1")) { _doCombo = true; }

        return this;
    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {

    }

    public void Enter()
    {
        _isAttacking = true;
        _doCombo = false;
        _player.Rigidbody.velocity = Vector3.zero;
        _player.Animator.SetTrigger("Attack");
        _player.StartCoroutine(Attack());
        
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.3f);
        _player.AttackBox.enabled = true;
        yield return new WaitForSeconds(0.4f);
        _player.AttackBox.enabled = false;

        if (_doCombo)
        {
            _player.Animator.SetTrigger("Combo");
            _player.StartCoroutine(Combo());
            _doCombo = false;
        } else
        {
            _isAttacking = false;
        }
    }

    private IEnumerator Combo()
    {
        yield return new WaitForSeconds(0.1f);
        _player.ComboBox.enabled = true;
        yield return new WaitForSeconds(0.4f);
        _player.ComboBox.enabled = false;

        _isAttacking = false;
    }

    public void Exit()
    {

    }
}
