using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : State
{
    private GameObject slashAnim;
    public override void Enter()
    {
        base.Enter();
        UpdateDirection();
        bossController.weaponCollider.gameObject.SetActive(true);
        animator.SetTrigger("MeleeAttack");
        bossController.enemyPathfinding.StopMoving();
        canTransition = true;
    }

    private void UpdateDirection()
    {
        if (bossController.transform.position.x - PlayerController.Instance.transform.position.x < 0)
        {
            bossController.spriteRenderer.flipX = false;
        }
        else
        {
            bossController.spriteRenderer.flipX = true;
        }
    }

    public override void Transition()
    {
        if (canTransition)
        {
            canTransition = false;
            bossController.ChangeState(bossController.followState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public void DoneAttackingAnimEvent()
    {
        bossController.weaponCollider.gameObject.SetActive(false);
    }
}
