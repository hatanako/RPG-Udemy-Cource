using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleMachine : PlayerGroundedState
{
    public PlayerIdleMachine(Player player, PlayerStateMachine playerStateMachine, string animBoolName) : base(player, playerStateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.ZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (!player.IsWallDetected() && xInput !=0 || (player.IsWallDetected() && xInput!=player.facingDir && xInput != 0))
        {
            player.stateMachine.ChangeState(player.moveState);
        }
    }
}
