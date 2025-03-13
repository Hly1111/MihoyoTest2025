
public class PlayerBlockState : PlayerReflectionState
{
    public PlayerBlockState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        ResetVelocity();
        StartAnimation(PlayerStateMachine.Player.AnimationData.BlockParameter);
    }
    
    public override void Exit()
    {
        base.Exit();
        StopAnimation(PlayerStateMachine.Player.AnimationData.BlockParameter);
    }
}