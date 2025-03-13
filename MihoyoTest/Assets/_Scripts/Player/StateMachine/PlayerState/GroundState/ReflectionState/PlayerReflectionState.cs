
public class PlayerReflectionState:PlayerGroundState
{
    protected PlayerReflectionState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        StartAnimation(PlayerStateMachine.Player.AnimationData.ReflectionParameter);
    }
    
    public override void Exit()
    {
        base.Exit();
        StopAnimation(PlayerStateMachine.Player.AnimationData.ReflectionParameter);
    }
}