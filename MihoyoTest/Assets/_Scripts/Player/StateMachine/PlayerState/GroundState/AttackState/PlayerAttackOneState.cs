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
        AddAnimEvent(EAnimNotify.OnAttackOneStartInput, AnimEvent_StartReceivingAttack);
        AddAnimEvent(EAnimNotify.OnAttackOneEndInput, AnimEvent_StopReceivingAttack); 
        AddAnimEvent(EAnimNotify.OnAttackOneEnd, AnimEvent_AnimComplete);
        StartAnimation(PlayerStateMachine.Player.AnimationData.AttackOneParameter);
    }
    
    public override void Exit()
    {
        base.Exit();
        RemoveAnimEvent(EAnimNotify.OnAttackOneStartInput, AnimEvent_StartReceivingAttack);
        RemoveAnimEvent(EAnimNotify.OnAttackOneEndInput, AnimEvent_StopReceivingAttack);
        RemoveAnimEvent(EAnimNotify.OnAttackOneEnd, AnimEvent_AnimComplete);
        StopAnimation(PlayerStateMachine.Player.AnimationData.AttackOneParameter);
    }

    protected override void AnimEvent_StopReceivingAttack()
    {
        base.AnimEvent_StopReceivingAttack();
        if (HasAttack)
        {
            PlayerStateMachine.ChangeState(PlayerStateMachine.AttackTwoState);
        }
    }

    protected override void AnimEvent_AnimComplete()
    {
        base.AnimEvent_AnimComplete();
        PlayerStateMachine.ChangeState(PlayerStateMachine.IdleState);
    }
}
