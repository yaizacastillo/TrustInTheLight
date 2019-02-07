using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabState : IEnemyState
{
    private Enemy enemy;

    public void Enter(Enemy e)
    {
        enemy = e;

        enemy.StopMovement();
        enemy.BlockPlayerActions();
        enemy.LookAtPlayer();
    }

    public void Execute()
    {
    }

    public void Exit()
    {
    }
}
