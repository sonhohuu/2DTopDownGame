using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : State
{
    private Transform weaponCollider;
    public override void Enter()
    {
        base.Enter();
        weaponCollider = bossController.weaponCollider;
    }

    public override void Transition()
    {
        float distanceToPlayer = Vector2.Distance(bossController.transform.position, PlayerController.Instance.transform.position);

        // Transition to MeleeAttackState if within melee attack range
        if (distanceToPlayer <= bossController.meleeAttackRange)
        {
            bossController.ChangeState(bossController.meleeAttackState);
            return;
        }

        // Transition to RangedAttackState if player is more than 100 units away
        if (distanceToPlayer > bossController.rangeAttackRange)
        {
            bossController.ChangeState(bossController.rangeAttackState);
            return;
        }

        // Determine direction to the player
        Vector2 direction = (PlayerController.Instance.transform.position - bossController.transform.position).normalized;

        // Flip sprite based on direction
        bool flipSprite = (direction.x < 0f); // Assuming x-axis determines direction
        if (flipSprite)
        {
            weaponCollider.localPosition = new Vector3(-1 * weaponCollider.localPosition.x, weaponCollider.localPosition.y, weaponCollider.localPosition.z); // Adjust position
            weaponCollider.localScale = new Vector3(-1 * weaponCollider.localScale.x, weaponCollider.localScale.y, weaponCollider.localScale.z);
        }
        // Apply sprite flip
        bossController.spriteRenderer.flipX = flipSprite;

        // Move towards the player
        bossController.MoveTo(direction);
    }
}
