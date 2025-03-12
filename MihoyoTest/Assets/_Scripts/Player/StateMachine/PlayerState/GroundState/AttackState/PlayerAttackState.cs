using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerAttackState : PlayerGroundState
{
    protected bool HasAttack;
    
    protected PlayerAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(PlayerStateMachine.Player.AnimationData.AttackParameter);
    }
    
    public override void Exit()
    {
        base.Exit();
        HasAttack = false;
        StopAnimation(PlayerStateMachine.Player.AnimationData.AttackParameter);
        
        ResetVelocity();
    }
    
    protected override void AddInputCallbacks()
    {
        PlayerStateMachine.Player.PlayerInput.GameplayActions.Attack.performed += AttackInput;
    }

    protected override void RemoveInputCallbacks()
    {
        PlayerStateMachine.Player.PlayerInput.GameplayActions.Attack.performed -= AttackInput;
    }
    
    
    protected virtual void AnimEvent_StartReceivingAttack()
    {
        AddInputCallbacks();
    }

    protected virtual void AnimEvent_StopReceivingAttack()
    {
        RemoveInputCallbacks();
    }

    protected virtual void AnimEvent_AnimComplete()
    {
        
    }
    
    protected virtual void AttackInput(InputAction.CallbackContext context)
    {
        HasAttack = true;
    }

    protected virtual void AddAttackForce()
    {
        Vector3 attackDirection = PlayerStateMachine.Player.Rigidbody.transform.forward;
        PlayerStateMachine.Player.Rigidbody.AddForce(attackDirection * PlayerStateMachine.ReusableData.AttackForce, ForceMode.Impulse);
    }
}
