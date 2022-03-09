using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected IState _currentState;

    void Update()
    {
        LogicUpdate();

        if (_currentState == null) { return; }
        ChangeState(_currentState.HandleInput());
        _currentState.Update();
    }

    protected virtual void LogicUpdate()
    {

    }

    private void FixedUpdate()
    {
        PhysicsUpdate();

        if (_currentState == null) { return; }
        _currentState.FixedUpdate();
    }

    protected virtual void PhysicsUpdate()
    {

    }

    public void ChangeState(IState state)
    {
        if (_currentState == state) { return; }
        if (_currentState != null)
        {
            _currentState.Exit();
        }
        _currentState = state;
        _currentState.Enter();
        print(_currentState.ToString());
    }
}
