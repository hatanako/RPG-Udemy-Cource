using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    
    public EnemyStateMachine stateMachine { get; private set; }
    public EnemyState currentState { get { return stateMachine.currentState; } }

    [SerializeField] public float idleTime;

    [SerializeField] private EnemyState startState;
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();
    }


    protected override void Update()
    {
        base.Update();
        currentState.Update();
    }

    public void ChangeState(EnemyState newState)
    {
        stateMachine.ChangeState(newState);
    }
}
