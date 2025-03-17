
using UnityEngine;

public class EnemyGroundState : EnemyState
{
    protected EnemyGroundState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        StartAnimation(EnemyStateMachine.EnemyAIController.EnemyAnimationData.GroundedParameter);
    }
    
    public override void Exit()
    {
        base.Exit();
        StopAnimation(EnemyStateMachine.EnemyAIController.EnemyAnimationData.GroundedParameter);
    }

    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
        if(collider.CompareTag("PlayerAttack"))
        {
            GetHit();
        }
    }

    private void GetHit()
    {
        EnemyStateMachine.ChangeState(EnemyStateMachine.EnemyHurtState);
    }
}