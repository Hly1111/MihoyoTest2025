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
        PlayerStateMachine.ReusableData.AttackIndex = 1;
        AddAttackForce();
        StartAnimation(PlayerStateMachine.Player.AnimationData.AttackOneParameter);
    }
    
    public override void Exit()
    {
        base.Exit();
        StopAnimation(PlayerStateMachine.Player.AnimationData.AttackOneParameter);
    }
    
    public void AttackDoneCallback(InputAction.CallbackContext obj)
    {
        PlayerStateMachine.ReusableData.AttackIndex = 2;
    }
}
