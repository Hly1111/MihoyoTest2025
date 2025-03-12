using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public EnemyAIController EnemyAIController { get; private set; }
    
    public EnemyIdleState EnemyIdleState { get; private set; }
    public EnemyPatrolState EnemyPatrolState { get; private set; }
    
    
    public EnemyStateMachine(EnemyAIController enemyAIController)
    {
        EnemyAIController = enemyAIController;
        
        EnemyIdleState = new EnemyIdleState(this);
        EnemyPatrolState = new EnemyPatrolState(this);
    }
}
