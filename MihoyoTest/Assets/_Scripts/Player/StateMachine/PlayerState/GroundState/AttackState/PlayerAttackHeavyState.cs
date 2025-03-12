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
        AddAnimEvent(EAnimNotify.OnAttackThreeEnd, AnimEvent_AnimComplete);
        AddAnimEvent(EAnimNotify.OnAttackThreeStartInput, AnimEvent_StartReceivingAttack);
        AddAnimEvent(EAnimNotify.OnAttackThreeEndInput, AnimEvent_StopReceivingAttack);
        StartAnimation(PlayerStateMachine.Player.AnimationData.AttackHeavyParameter);
    }
    
    public override void Exit()
    {
        base.Exit();
        RemoveAnimEvent(EAnimNotify.OnAttackThreeStartInput, AnimEvent_StartReceivingAttack);
        RemoveAnimEvent(EAnimNotify.OnAttackThreeEndInput, AnimEvent_StopReceivingAttack);
        RemoveAnimEvent(EAnimNotify.OnAttackThreeEnd, AnimEvent_AnimComplete);
        RemovePreInputCallback();
        StopAnimation(PlayerStateMachine.Player.AnimationData.AttackHeavyParameter);
    }

    protected override void AnimEvent_StopReceivingAttack()
    {
        base.AnimEvent_StopReceivingAttack();
        if (HasAttack)
        {
            PlayerStateMachine.ChangeState(PlayerStateMachine.AttackOneState);
        }

        AddPreInputCallback();
    }

    protected override void AnimEvent_AnimComplete()
    {
        base.AnimEvent_AnimComplete();
        PlayerStateMachine.ChangeState(PlayerStateMachine.IdleState);
    }
}
