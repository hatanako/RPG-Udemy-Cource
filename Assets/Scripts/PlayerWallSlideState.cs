using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{

    private float inputBufferTime = 0.15f; // 输入缓冲时间
    private float stateStartTime;
    private bool wallOnLeft; // 记录墙面在左侧还是右侧
    public PlayerWallSlideState(Player player, PlayerStateMachine playerStateMachine, string animBoolName) : base(player, playerStateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateStartTime = Time.time; // 记录状态开始时间
        wallOnLeft = player.facingDir == 1; // 根据玩家朝向确定墙面位置
    }

    public override void Exit()
    {
        
        base.Exit();
    }

    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJump);
            return;
        }

        // 输入缓冲处理
        bool isBufferActive = Time.time < stateStartTime + inputBufferTime;
        
        if (!isBufferActive && xInput != 0 && xInput != player.facingDir)
            stateMachine.ChangeState(player.idleState);

        if (yInput < 0)
            rb.velocity = new Vector2(0, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y * .7f);

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);

        base.Update();
    }
}
