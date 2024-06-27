using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    public override void Enter()
    {
        base.Enter();
        animator.Play("Death");
        bossController.enemyPathfinding.StopMoving();
    }
}
