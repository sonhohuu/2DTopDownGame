using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArmorBuffState : State
{
    protected override void Awake()
    {
        base.Awake();
    }
    public override void Enter()
    {
        base.Enter();
        animator.Play("ArmorBuff");
        bossController.enemyPathfinding.StopMoving();
        StartCoroutine(WaitForAnimation());
    }
    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        canTransition = true;
    }

    public override void Transition()
    {
        if (canTransition)
        {
            canTransition = false;
            bossController.ChangeState(bossController.followState);
        }
    }
}
