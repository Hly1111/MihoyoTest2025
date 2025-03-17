
public class PlayerKillEndPoseState : PlayerReflectionState
{
    public PlayerKillEndPoseState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        StartAnimation(PlayerStateMachine.Player.AnimationData.KillEndPoseParameter);
    }
    
    public override void Exit()
    {
        base.Exit();
        StopAnimation(PlayerStateMachine.Player.AnimationData.KillEndPoseParameter);
        CameraManager.Instance.DeactivateStateCamera();
    }
}