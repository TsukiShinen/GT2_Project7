using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistanceAttack : IState
{
    Player _player;

    public PlayerDistanceAttack(Player player)
    {
        _player = player;
    }

    public IState HandleInput()
    {


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

    }

    public void Exit()
    {

    }
}
