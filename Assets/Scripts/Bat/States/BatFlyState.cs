using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatFlyState : IState
{
    private BatController _bat;

    public BatFlyState(BatController bat)
    {
        _bat = bat;
    }
    public void HandleInput()
    {

    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {

    }

    public void Enter()
    {
        _bat.Animator.SetBool("Fly", true);
    }

    public void Exit()
    {
        _bat.Animator.SetBool("Fly", false);
    }
}
