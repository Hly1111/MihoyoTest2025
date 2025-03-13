using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerAttackHeavyState : PlayerAttackState
{
    public PlayerAttackHeavyState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        StartAnimation(PlayerStateMachine.Player.AnimationData.AttackHeavyParameter);
    }
    
    public override void Exit()
    {
        base.Exit();
        RemovePreInputCallback();
        StopAnimation(PlayerStateMachine.Player.AnimationData.AttackHeavyParameter);
    }
}
