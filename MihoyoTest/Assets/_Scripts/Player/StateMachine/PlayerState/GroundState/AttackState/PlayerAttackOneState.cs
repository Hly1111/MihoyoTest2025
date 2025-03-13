using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackOneState : PlayerAttackState
{
    public PlayerAttackOneState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AddAttackForce();
        StartAnimation(PlayerStateMachine.Player.AnimationData.AttackOneParameter);
    }
    
    public override void Exit()
    {
        base.Exit();
        StopAnimation(PlayerStateMachine.Player.AnimationData.AttackOneParameter);
    }
}
