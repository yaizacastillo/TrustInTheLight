using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScapeState : IEnemyState
{
    private Enemy enemy;

    public void Enter(Enemy e)
    {
        enemy = e;

        //Cambiar sonido
        enemy.PlayScapeSound();
        //Cambiar textura
        enemy.ChangeScapeMaterial();

        enemy.UnblockPlayerActions();
        enemy.DetectScapePoint();
    }

    public void Execute()
    {
        enemy.RunToScapePoint();

        if (Vector3.Distance(enemy.transform.position, enemy.GetScapePoint()) < 1)
        {
            enemy.Destroy();
        }
    }

    public void Exit()
    {
    }
}
