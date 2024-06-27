using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms;

public class BossController : MonoBehaviour
{
    public enum BossState
    {
        Idle,
        Follow,
        MeleeAttack,
        HomingMissile,
        ArmorBuff,
        Dash,
        Death
    }

    public State idleState;
    public State followState;
    public State meleeAttackState;
    public State dashState;
    public State rangeAttackState;
    public State armorBuffState;
    public State deathState;
    // Add other states here

    [SerializeField] public float roamChangeDirFloat = 2f;
    [SerializeField] public float moveSpeed = 2f;
    [SerializeField] public float followSpeed = 3f;
    [SerializeField] public float followRange = 15f;
    [SerializeField] public float meleeAttackRange = 4f;
    [SerializeField] public float rangeAttackRange = 7f;
    [SerializeField] public float armorBuffDuration = 10f;
    [SerializeField] public float armorBuffStartTime;
    [SerializeField] public bool stopMovingWhileAttacking = false;
    [SerializeField] public Transform weaponCollider;

    protected Animator animator;
    public SpriteRenderer spriteRenderer;
    protected Vector2 roamPosition;
    protected float timeRoaming = 0f;
    public BossState state;
    public float attackCooldown = 2f;
    protected bool isDashing = false;
    protected float startingMoveSpeed;

    public EnemyPathFinding enemyPathfinding;

    private State currentState;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathFinding>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        idleState = GetComponentInChildren<IdleState>();
        followState = GetComponentInChildren<FollowState>();
        meleeAttackState = GetComponentInChildren<MeleeAttackState>();
        dashState = GetComponentInChildren<DashState>();
        rangeAttackState = GetComponentInChildren<RangedAttackState>();
        armorBuffState = GetComponentInChildren<ArmorBuffState>();
        deathState  = GetComponentInChildren<DeathState>();
        // Initialize other states similarly

        InitializeStates();
        currentState = idleState;
    }
    private void InitializeStates()
    {
        idleState.Initialize(this);
        followState.Initialize(this);
        meleeAttackState.Initialize(this);
        dashState.Initialize(this);
        rangeAttackState.Initialize(this);
        armorBuffState.Initialize(this);
        deathState.Initialize(this);
        // Initialize other states similarly
    }

    private void Start()
    {
        roamPosition = GetRoamingPosition();
        currentState.Enter();
    }

    private void Update()
    {
        currentState.Transition();
    }

    public void ChangeState(State newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    public Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    public void MoveTo(Vector2 direction)
    {
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + direction, moveSpeed * Time.deltaTime);
    }
}
