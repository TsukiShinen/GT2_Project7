using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public IState _currentState;

    public virtual void Update()
    {
        if (_currentState == null) { return; }
        ChangeState(_currentState.HandleInput());
        _currentState.Update();
    }

    public virtual void FixedUpdate()
    {
        if (_currentState == null) { return; }
        _currentState.FixedUpdate();
    }

    protected void ChangeState(IState state)
    {
        if (_currentState == state) { return; }
        if (_currentState != null)
        {
            _currentState.Exit();
        }
        _currentState = state;
        _currentState.Enter();
    }
}
