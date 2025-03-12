
using UnityEngine;

public class EnemyMovementState : EnemyState
{
    protected EnemyMovementState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("EnemyMovementState Enter:" + this);
        StartAnimation(EnemyStateMachine.EnemyAIController.EnemyAnimationData.MoveParameter);
    }
    
    public override void Exit()
    {
        base.Exit();
        StopAnimation(EnemyStateMachine.EnemyAIController.EnemyAnimationData.MoveParameter);
    }
}