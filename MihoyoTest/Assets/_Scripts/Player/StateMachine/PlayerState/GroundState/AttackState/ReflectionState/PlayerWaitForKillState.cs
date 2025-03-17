
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWaitForKillState : PlayerReflectionState
{
    public PlayerWaitForKillState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        PlayerStateMachine.ReusableData.TargetEnemy.SetKillState();
        CameraManager.Instance.ActivateStateCamera();
        PlayerStateMachine.ReusableData.SpeedModifier = 0f;
        StartAnimation(PlayerStateMachine.Player.AnimationData.WaitForKillParameter);
    }
    
    public override void Exit()
    {
        base.Exit();
        StopAnimation(PlayerStateMachine.Player.AnimationData.WaitForKillParameter);
    }
}