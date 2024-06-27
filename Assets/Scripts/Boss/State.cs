using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected BossController bossController;
    protected Animator animator;
    protected bool canTransition = false;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Initialize(BossController controller)
    {
        bossController = controller;
    }

    public virtual void Enter()
    {
        enabled = true;
    }

    public virtual void Exit()
    {
        enabled = false;
    }

    public virtual void Transition()
    {
        // Each state will implement its own transition logic
    }
}
