using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Attack details")]
    public Vector2[] attackMovement;

    public bool isBusy { get; private set; }
    [Header("Move Info")]
    public float moveSpeed = 12f;
    public float jumpForce;

    [Header("Dash Info")]
    [SerializeField] private float dashCoolDown;
    private float dashUsageTimer;
    public float dashSpeed;
    public float dashDuration;
    public float dashDir {  get; private set; }

    [Header("Collision Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckInstence;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckInstence;
    [SerializeField] private LayerMask whatIsGround;

    public int facingDir { get; private set; } = 1;
    public bool facingRight = true;

    #region Components
    public Animator animator { get; private set; }
    public Rigidbody2D rb {  get; private set; }
    #endregion

    #region States
    public PlayerStateMachine stateMachine {  get; private set; }

    public PlayerMoveState moveState { get; private set; }

    public PlayerIdleMachine idleState { get; private set; }

    public PlayerWallJumpState wallJump { get; private set; }

    public PlayerJumpState jumpState { get; private set; }

    public PlayerAirState airState { get; private set; }

    public PlayerDashState dashState { get; private set; }

    public PlayerWallSlideState wallSlideState { get; private set; }

    public PlayerPrimaryAttackState playerPrimaryAttack { get; private set; }
    #endregion

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleMachine(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJump = new PlayerWallJumpState(this, stateMachine, "Jump");

        playerPrimaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        stateMachine.Initialize(idleState);

    }


    private void Update()
    {
        stateMachine.currentState.Update();
        IsGroundDetected();
        CheckForDashInput();

    }

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    private void CheckForDashInput()
    {
        if(IsWallDetected()) 
            return;

        dashUsageTimer -= Time.deltaTime;

        if ( Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0)
        {

            dashUsageTimer = dashCoolDown;
            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
                dashDir = facingDir;

            stateMachine.ChangeState(dashState);
        }
    }
    #region Velocity
    public void ZeroVelocity() => rb.velocity = new Vector2(0, 0);

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    #endregion

    #region Collision
    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down,groundCheckInstence, whatIsGround);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckInstence, whatIsGround);

    public void OnDrawGizmos()
    {
        DrawLine(groundCheck, groundCheckInstence, false);
        DrawLine(wallCheck, wallCheckInstence, true);  
    }

    public void DrawLine(Transform _check,float _instence,bool _horizontal)
    {
        if (_horizontal)
        {
            Gizmos.DrawLine(_check.position, new Vector3(_check.position.x + _instence, _check.position.y));
        }
        else 
        {
            Gizmos.DrawLine(_check.position, new Vector3(_check.position.x, _check.position.y - _instence));
        }
    }
    #endregion

    #region Flip
    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
        {
            Flip();
        }
        else if (_x < 0 && facingRight)
        {
            Flip();
        }
    }
}
#endregion