using UnityEngine;

public class EnemyPatrolState : EnemyMovementState
{
    public EnemyPatrolState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        StartAnimation(EnemyStateMachine.EnemyAIController.EnemyAnimationData.RunParameter);
    }
    
    public override void Exit()
    {
        base.Exit();
        StopAnimation(EnemyStateMachine.EnemyAIController.EnemyAnimationData.RunParameter);
    }
    
    public override void Update()
    {
        base.Update();
        if (!EnemyStateMachine.EnemyAIController.EnemyAIKnowledge.ShouldMove)
        {
            EnemyStateMachine.ChangeState(EnemyStateMachine.EnemyIdleState);
        }
    }
}
