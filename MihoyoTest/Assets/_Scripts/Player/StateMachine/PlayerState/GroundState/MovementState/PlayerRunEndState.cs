
public class PlayerRunEndState: PlayerMovementState
{
    public PlayerRunEndState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        PlayerStateMachine.ReusableData.SpeedModifier = 0f;
        ResetVelocity();
        
        AddAnimEvent(EAnimNotify.OnRunEndStartPreInput,AnimEvent_OnRunEndStartPreInput);
        AddAnimEvent(EAnimNotify.OnRunEndEnd,AnimEvent_OnRunEndEnd);
        StartAnimation(PlayerStateMachine.Player.AnimationData.RunEndParameter);
    }

    public override void Exit()
    {
        base.Exit();
        RemoveAnimEvent(EAnimNotify.OnRunEndStartPreInput,AnimEvent_OnRunEndStartPreInput);
        RemoveAnimEvent(EAnimNotify.OnRunEndEnd,AnimEvent_OnRunEndEnd);
        StopAnimation(PlayerStateMachine.Player.AnimationData.RunEndParameter);
    }

    private void AnimEvent_OnRunEndStartPreInput()
    {
        AddPreInputCallback();
    }
    
    private void AnimEvent_OnRunEndEnd()
    {
        RemovePreInputCallback();
        PlayerStateMachine.ChangeState(PlayerStateMachine.IdleState);
    }
}
