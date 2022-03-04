using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected IState _currentState;

    void Update()
    {
        _currentState.HandleInput();
        _currentState.Update();

        LogicUpdate();
    }

    protected virtual void LogicUpdate()
    {

    }

    private void FixedUpdate()
    {
        _currentState.FixedUpdate();

        PhysicsUpdate();
    }

    protected virtual void PhysicsUpdate()
    {

    }

    public void ChangeState(IState state)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }
        _currentState = state;
        _currentState.Enter();
    }
}
