using System;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class EnemyAIKnowledge : IDisposable
{
    public EnemyAIController AIController;
    [field: SerializeField] public bool ShouldMove;
    [field: SerializeField] public bool ShouldAttack;
    [field: SerializeField] public EnemyTargetKnowledge TargetKnowledge;
    [field: SerializeField] public EnemyPathKnowledge PathKnowledge;

    public void Initialize(PlayerController target, EnemyAIController enemyAIController)
    {
        ShouldMove = false;
        ShouldAttack = false;
        AIController = enemyAIController;
        
        TargetKnowledge = new EnemyTargetKnowledge();
        TargetKnowledge.Initialize();
        
        PathKnowledge = new EnemyPathKnowledge();
        PathKnowledge.Initialize(AIController.transform.root.GetComponent<NavMeshAgent>());
    }

    
    public void Dispose()
    {
        AIController = null;
        ShouldMove = false;
        ShouldAttack = false;
        
        TargetKnowledge.Dispose();
        TargetKnowledge = null;
        
        PathKnowledge.Dispose();
        PathKnowledge = null;
    }
}
