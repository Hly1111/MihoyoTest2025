
using UnityEngine;

public class PlayerReflectionState :PlayerAttackState
{
    protected PlayerReflectionState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        PlayerStateMachine.ReusableData.SpeedModifier = 0f;
        StartAnimation(PlayerStateMachine.Player.AnimationData.ReflectionParameter);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(PlayerStateMachine.Player.AnimationData.ReflectionParameter);
    }
}