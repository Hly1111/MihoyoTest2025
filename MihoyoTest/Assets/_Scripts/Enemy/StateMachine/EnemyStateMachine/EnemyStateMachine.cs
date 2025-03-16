using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public EnemyAIController EnemyAIController { get; private set; }
    
    public EnemyIdleState EnemyIdleState { get; private set; }
    public EnemyPatrolState EnemyPatrolState { get; private set; }
    
    public EnemyAttackState EnemyAttackState { get; private set; }
    
    
    public EnemyStateMachine(EnemyAIController enemyAIController)
    {
        EnemyAIController = enemyAIController;
        
        EnemyIdleState = new EnemyIdleState(this);
        EnemyPatrolState = new EnemyPatrolState(this);
        EnemyAttackState = new EnemyAttackState(this);
        
        BindAllEvents();
    }
    
    private void BindAllEvents()
    {
        EnemyAIController.EnemyAnimEventHandler.BindAllAnimEvents(this);
    }

    private void UnbindAllEvents()
    {
        EnemyAIController.EnemyAnimEventHandler.UnbindAllAnimEvents(this);
    }
    
    private void AnimEvent_OnAttack()
    {
        EnemyAttackState.LaunchProjectile();
    }

    private void AnimEvent_OnAttackEnd()
    {
        ChangeState(EnemyIdleState);
    }
}
