using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public bool FacingLeft { get { return facingLeft; } }

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer myTrailRenderer;

    private PlayerControlls playerControlls;
    private Vector2 movement;
    private Rigidbody2D rigidbody2D;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRender;
    private float startingMoveSpeed;

    private bool facingLeft = false;
    private bool isDashing = false;

    protected override void Awake()
    {
        base.Awake();

        playerControlls = new PlayerControlls();
        rigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRender = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        playerControlls.Combat.Dash.performed += _ => Dash();

        startingMoveSpeed = moveSpeed;
    }

    private void OnEnable()
    {
        playerControlls.Enable();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControlls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        rigidbody2D.MovePosition(rigidbody2D.position + movement * (moveSpeed * Time.deltaTime));
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            mySpriteRender.flipX = true;
            facingLeft = true;
        }
        else
        {
            mySpriteRender.flipX = false;
            facingLeft = false;
        }
    }

    private void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            moveSpeed *= dashSpeed;
            myTrailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine()
    {
        float dashTime = .2f;
        float dashCD = .25f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = startingMoveSpeed;
        myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
}
