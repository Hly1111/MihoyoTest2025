using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackTwoState : PlayerAttackState
{
    public PlayerAttackTwoState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        AddAttackForce();
        StartAnimation(PlayerStateMachine.Player.AnimationData.AttackTwoParameter);
    }
    
    public override void Exit()
    {
        base.Exit();
        StopAnimation(PlayerStateMachine.Player.AnimationData.AttackTwoParameter);
    }
    
    public void AttackDoneCallback(InputAction.CallbackContext obj)
    {
        PlayerStateMachine.ReusableData.AttackIndex = 3;
    }
}
