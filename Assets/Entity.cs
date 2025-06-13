using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    [Header("Collision Check")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckInstence;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckInstence;
    [SerializeField] protected LayerMask whatIsGround;

    #region Components
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

    [Header("Move Info")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float jumpForce;

    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {

    }

    #region Collision
    public virtual bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckInstence, whatIsGround);
    public virtual bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckInstence, whatIsGround);

    protected virtual void OnDrawGizmos()
    {
        DrawLine(groundCheck, groundCheckInstence, false);
        DrawLine(wallCheck, wallCheckInstence, true);
    }

    protected virtual void DrawLine(Transform _check, float _instence, bool _horizontal)
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
    #endregion
    #region Velocity
    public void ZeroVelocity() => rb.velocity = new Vector2(0, 0);

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    #endregion
}
