using UnityEngine;

public class PlayerJumpEndState : PlayerGroundState
{
    public PlayerJumpEndState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        ResetVelocity();
        StartAnimation(PlayerStateMachine.Player.AnimationData.JumpEndParameter);
    }

    public override void Exit()
    {
        base.Exit();
        RemovePreInputCallback();
        StopAnimation(PlayerStateMachine.Player.AnimationData.JumpEndParameter);
    }
}
