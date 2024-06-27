using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    private Vector2 roamPosition;
    private float timeRoaming = 0f;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Enter()
    {
        base.Enter();
        roamPosition = GetRoamingPosition();
    }

    public override void Transition()
    {
        timeRoaming += Time.deltaTime;

        bossController.MoveTo(roamPosition);

        // Flip the sprite based on the direction of movement
        Vector2 direction = roamPosition - (Vector2)bossController.transform.position;
        if (direction.x != 0)
        {
            bossController.spriteRenderer.flipX = direction.x < 0;
        }

        // Transition to follow state if the player is within follow range
        if (Vector2.Distance(bossController.transform.position, PlayerController.Instance.transform.position) < bossController.followRange)
        {
            bossController.ChangeState(bossController.followState);
        }

        // Change roam position after a certain time
        if (timeRoaming > bossController.roamChangeDirFloat)
        {
            roamPosition = GetRoamingPosition();
            timeRoaming = 0f;
        }
    }

    private Vector2 GetRoamingPosition()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float roamRadius = 5.0f; // Adjust the roam radius as needed
        return (Vector2)bossController.transform.position + randomDirection * roamRadius;
    }
}
