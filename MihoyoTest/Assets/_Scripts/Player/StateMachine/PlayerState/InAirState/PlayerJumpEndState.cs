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
        AddAnimEvent(EAnimNotify.OnJumpEndEnd, AnimEvent_JumpEndComplete);
        AddAnimEvent(EAnimNotify.OnJumpEndStartPreInput, AnimEvent_JumpEndStartPreInput);
        StartAnimation(PlayerStateMachine.Player.AnimationData.JumpEndParameter);
    }

    public override void Exit()
    {
        base.Exit();
        RemoveAnimEvent(EAnimNotify.OnJumpEndEnd, AnimEvent_JumpEndComplete);
        RemoveAnimEvent(EAnimNotify.OnJumpEndStartPreInput, AnimEvent_JumpEndStartPreInput);
        RemovePreInputCallback();
        StopAnimation(PlayerStateMachine.Player.AnimationData.JumpEndParameter);
    }

    private void AnimEvent_JumpEndComplete()
    {
        PlayerStateMachine.ChangeState(PlayerStateMachine.IdleState);
    }

    private void AnimEvent_JumpEndStartPreInput()
    {
        AddPreInputCallback();
    }
}
