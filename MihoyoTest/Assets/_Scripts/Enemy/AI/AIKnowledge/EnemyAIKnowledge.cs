using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[Serializable]
public class EnemyAIKnowledge : IDisposable
{
    public EnemyAIController aiController;
    [field: SerializeField] public EnemyTargetKnowledge targetKnowledge;
    [field: SerializeField] public EnemyPathKnowledge pathKnowledge;

    public void Initialize(EnemyAIController enemyAIController)
    {
        aiController = enemyAIController;
        
        targetKnowledge = new EnemyTargetKnowledge();
        targetKnowledge.Initialize();
        
        pathKnowledge = new EnemyPathKnowledge();
        pathKnowledge.Initialize(aiController.transform.root.GetComponent<NavMeshAgent>());
    }

    
    public void Dispose()
    {
        aiController = null;
        
        targetKnowledge.Dispose();
        targetKnowledge = null;
        
        pathKnowledge.Dispose();
        pathKnowledge = null;
    }
}
