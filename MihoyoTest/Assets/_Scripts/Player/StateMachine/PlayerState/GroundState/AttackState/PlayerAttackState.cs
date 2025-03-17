using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerAttackState : PlayerGroundState
{
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
    
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (PlayerStateMachine.ReusableData.TargetEnemy)
        {
            Vector3 direction = (PlayerStateMachine.ReusableData.TargetEnemy.transform.position - PlayerStateMachine.Player.transform.position).normalized;
            Rotate(direction, false);
        }
    }

    protected virtual void AddAttackForce()
    {
        Vector3 attackDirection = PlayerStateMachine.Player.Rigidbody.transform.forward;
        PlayerStateMachine.Player.Rigidbody.AddForce(attackDirection * PlayerStateMachine.ReusableData.AttackForce, ForceMode.Impulse);
    }
}
