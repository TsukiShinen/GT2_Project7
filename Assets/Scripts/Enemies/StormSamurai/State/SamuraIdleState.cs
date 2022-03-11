using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiIdleState : IState
{
    private SamuraiController _samurai;

    public SamuraiIdleState(SamuraiController samurai)
    {
        _samurai = samurai;
    }

    public IState HandleInput()
    {
        if (_samurai.Detection.IsPlayerDetected)
            return _samurai.RunState;
        else
            return this;
    }

    public void Update()
    {
        if (DayNightManager.Instance.IsDay)
            _samurai.SpriteRenderer.material = _samurai.DayMaterial;
        else
            _samurai.SpriteRenderer.material = _samurai.NightMaterial;
    }

    public void FixedUpdate()
    {

    }

    public void Enter()
    {

    }

    public void Exit()
    {

    }
}
