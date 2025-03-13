
public class PlayerKillState : PlayerReflectionState
{
    public PlayerKillState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        StartAnimation(PlayerStateMachine.Player.AnimationData.KillParameter);
    }
    
    public override void Exit()
    {
        base.Exit();
        StopAnimation(PlayerStateMachine.Player.AnimationData.KillParameter);
    }
}