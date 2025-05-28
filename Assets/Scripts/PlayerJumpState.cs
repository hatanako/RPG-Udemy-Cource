using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player, PlayerStateMachine playerStateMachine, string animBoolName) : base(player, playerStateMachine, animBoolName)
    {
    }
    bool isIdleState = false;
    public override void Enter()
    {
        base.Enter();

        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
        if (xInput == 0)
            isIdleState = true;
    }

    public override void Update()
    {
        base.Update();

        if (isIdleState&&xInput == player.facingDir&&player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState);
        if (rb.velocity.y <= 0)
            stateMachine.ChangeState(player.airState);
        if (xInput != 0)
            player.SetVelocity(player.moveSpeed * .8f * xInput, rb.velocity.y);


    }

    public override void Exit()
    {
        base.Exit();
    }
}
