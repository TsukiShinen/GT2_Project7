using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    public void HandleInput();
    public void Update();
    public void FixedUpdate();
    public void Enter();
    public void Exit();
}