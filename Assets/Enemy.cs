using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    
    [SerializeField] protected LayerMask whatIsPlayer;

    [Header("Move info")]
    [SerializeField] public float idleTime;

    [Header("Attack info")]
    [SerializeField] public float attackDistance;
    [SerializeField] protected Transform attackCheck; 
    public EnemyStateMachine stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }


    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public void ChangeState(EnemyState newState)
    {
        stateMachine.ChangeState(newState);
    }

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 50, whatIsPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        DrawLine(attackCheck, attackDistance, true);
    }
}
