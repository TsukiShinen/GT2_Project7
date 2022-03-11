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
        
    }

    public void FixedUpdate()
    {

    }

    public void Enter()
    {
        _samurai.StartCoroutine(UpdateMaterial());
    }

    public void Exit()
    {

    }

    private IEnumerator UpdateMaterial()
    {
        float duration = 0f;
        while (duration <= 2f)
        {
            if (DayNightManager.Instance.IsDay && _samurai.SpriteRenderer.material.GetFloat("_Intensity") > 0)
            _samurai.SpriteRenderer.material.SetFloat("_Intensity", duration);
            else if (!DayNightManager.Instance.IsDay && _samurai.SpriteRenderer.material.GetFloat("_Intensity") < 2)
                _samurai.SpriteRenderer.material.SetFloat("_Intensity", duration);
            duration += Time.deltaTime;
            yield return null;
        }
        _samurai.StartCoroutine(UpdateMaterial());
    }
}
