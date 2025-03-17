using UnityEngine;

public class EnemyIdleState : EnemyMovementState
{
    public EnemyIdleState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        StartAnimation(EnemyStateMachine.EnemyAIController.EnemyAnimationData.IdleParameter);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(EnemyStateMachine.EnemyAIController.EnemyAnimationData.IdleParameter);
    }
    
    public override void Update()
    {
        base.Update();
        EnemyStateMachine.EnemyAIController.EnemyAttackPerception.UpdateDecision();
        
        if (EnemyStateMachine.EnemyAIController.EnemyAIKnowledge.pathKnowledge.shouldMove)
        {
            EnemyStateMachine.ChangeState(EnemyStateMachine.EnemyPatrolState);
        }

        if (EnemyStateMachine.EnemyAIController.EnemyAIKnowledge.attackKnowledge.canAttack)
        {
            if (EnemyStateMachine.EnemyAIController.EnemyAIKnowledge.attackKnowledge.shouldAttack)
            {
                EnemyStateMachine.ChangeState(EnemyStateMachine.EnemyAttackState);
            }
        }
    }
}
