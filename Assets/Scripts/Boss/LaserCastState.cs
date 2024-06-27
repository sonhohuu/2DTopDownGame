using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCastState : State
{
    private Transform pivot;

    protected override void Awake()
    {
        base.Awake();
        pivot = transform.parent.Find("Pivot"); // Adjust the path based on your Unity hierarchy
    }

    public override void Enter()
    {
        base.Enter();
        StartCoroutine(PlayAnimations());
    }

    private IEnumerator PlayAnimations()
    {
        yield return PlayAnimation("laser_cast");
        yield return PlayAnimation("laser");
        canTransition = true;
    }

    private IEnumerator PlayAnimation(string animName)
    {
        animator.Play(animName);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
    }

    public override void Transition()
    {
        if (canTransition)
        {
            canTransition = false;
            bossController.ChangeState(bossController.GetComponentInChildren<DashState>());
        }
    }

    private void SetTarget()
    {
        pivot.rotation = Quaternion.LookRotation(Vector3.forward, PlayerController.Instance.transform.position - pivot.position);
    }
}
