using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerAttackState : PlayerGroundState
{
    public bool HasAttack;
    protected PlayerAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        ResetVelocity();
        StartAnimation(PlayerStateMachine.Player.AnimationData.AttackParameter);
    }
    
    public override void Exit()
    {
        base.Exit();
        StopAnimation(PlayerStateMachine.Player.AnimationData.AttackParameter);
        
        ResetVelocity();
    }

    protected virtual void AddAttackForce()
    {
        Vector3 attackDirection = PlayerStateMachine.Player.Rigidbody.transform.forward;
        PlayerStateMachine.Player.Rigidbody.AddForce(attackDirection * PlayerStateMachine.ReusableData.AttackForce, ForceMode.Impulse);
    }
    
    public void AttackDoneCallback(InputAction.CallbackContext obj)
    {
        HasAttack = true;
    }
}
