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
        ResetVelocity();
        
        AddAnimEvent(EAnimNotify.OnAttackTwoStartInput, AnimEvent_StartReceivingAttack);
        AddAnimEvent(EAnimNotify.OnAttackTwoEndInput, AnimEvent_StopReceivingAttack); 
        AddAnimEvent(EAnimNotify.OnAttackTwoEnd, AnimEvent_AnimComplete);
        StartAnimation(PlayerStateMachine.Player.AnimationData.AttackTwoParameter);
    }
    
    public override void Exit()
    {
        base.Exit();
        RemoveAnimEvent(EAnimNotify.OnAttackTwoStartInput, AnimEvent_StartReceivingAttack);
        RemoveAnimEvent(EAnimNotify.OnAttackTwoEndInput, AnimEvent_StopReceivingAttack);
        RemoveAnimEvent(EAnimNotify.OnAttackTwoEnd, AnimEvent_AnimComplete);
        StopAnimation(PlayerStateMachine.Player.AnimationData.AttackTwoParameter);
    }
    
    protected override void AnimEvent_StopReceivingAttack()
    {
        base.AnimEvent_StopReceivingAttack();
        if (HasAttack)
        {
            PlayerStateMachine.ChangeState(PlayerStateMachine.AttackHeavyState);
        }
    }
    
    protected override void AnimEvent_AnimComplete()
    {
        base.AnimEvent_AnimComplete();
        PlayerStateMachine.ChangeState(PlayerStateMachine.IdleState);
    }
}
