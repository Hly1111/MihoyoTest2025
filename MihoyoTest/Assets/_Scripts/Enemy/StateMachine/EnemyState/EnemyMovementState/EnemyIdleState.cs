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
        if (EnemyStateMachine.EnemyAIController.EnemyAIKnowledge.ShouldMove)
        {
            EnemyStateMachine.ChangeState(EnemyStateMachine.EnemyPatrolState);
        }
    }
}
