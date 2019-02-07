using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IEnemyState
{
    private Enemy enemy;

    public void Enter(Enemy e)
    {
        enemy = e;
    }

    public void Execute()
    {
        enemy.ChasePlayer();

        if (enemy.IsPlayerInRange())
            enemy.ChangeState(new GrabState());
    }

    public void Exit()
    {
    }

}
