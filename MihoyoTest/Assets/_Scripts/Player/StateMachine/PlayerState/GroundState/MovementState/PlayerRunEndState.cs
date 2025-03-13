
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
        StartAnimation(PlayerStateMachine.Player.AnimationData.RunEndParameter);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(PlayerStateMachine.Player.AnimationData.RunEndParameter);
    }
}
