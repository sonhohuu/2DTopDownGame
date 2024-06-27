using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DashState : State
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void Enter()
    {
        base.Enter();
        animator.Play("Dash");
        StartCoroutine(Dash());
    }

    private IEnumerator Dash()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = PlayerController.Instance.transform.position;
        float duration = 0.8f;
        float elapsedTime = 0f;

        // Determine direction for sprite flipping
        bool flipSprite = (endPos.x < startPos.x); // Assuming x-axis determines direction

        // Flip sprite initially
        bossController.spriteRenderer.flipX = flipSprite;

        while (elapsedTime < duration)
        {
            // Update position
            transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);

            // Flip sprite if direction changes
            bool newFlipSprite = (endPos.x < transform.position.x);
            if (newFlipSprite != flipSprite)
            {
                flipSprite = newFlipSprite;
                bossController.spriteRenderer.flipX = flipSprite;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final position is exact
        transform.position = endPos;

        // Enable transition after completing dash
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
