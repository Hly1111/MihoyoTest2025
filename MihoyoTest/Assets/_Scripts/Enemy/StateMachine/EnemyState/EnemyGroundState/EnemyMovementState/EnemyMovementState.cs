
using UnityEngine;

public class EnemyMovementState : EnemyGroundState
{
    protected EnemyMovementState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(EnemyStateMachine.EnemyAIController.EnemyAnimationData.MoveParameter);
    }
    
    public override void Exit()
    {
        base.Exit();
        StopAnimation(EnemyStateMachine.EnemyAIController.EnemyAnimationData.MoveParameter);
    }
    
    public override void Update()
    {
        base.Update();
        
        EnemyStateMachine.EnemyAIController.EnemyPathPerception.UpdateDecision();
        EnemyStateMachine.EnemyAIController.EnemyTargetPerception.UpdateDecision();
    }
}