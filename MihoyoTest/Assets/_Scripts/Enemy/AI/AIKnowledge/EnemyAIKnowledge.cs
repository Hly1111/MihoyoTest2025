using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[Serializable]
public class EnemyAIKnowledge : IDisposable
{
    public EnemyAIController aiController;
    [field: SerializeField] public Transform killTransform;
    [field: SerializeField] public EnemyTargetKnowledge targetKnowledge;
    [field: SerializeField] public EnemyPathKnowledge pathKnowledge;
    [field: SerializeField] public EnemyAttackKnowledge attackKnowledge;

    public void Initialize(EnemyAIController enemyAIController)
    {
        aiController = enemyAIController;
        killTransform = aiController.transform.root.Find("KillTransform");
        
        targetKnowledge = new EnemyTargetKnowledge();
        targetKnowledge.Initialize();
        
        pathKnowledge = new EnemyPathKnowledge();
        pathKnowledge.Initialize(aiController.transform.root.GetComponent<NavMeshAgent>());
        
        attackKnowledge = new EnemyAttackKnowledge();
        attackKnowledge.Initialize();
    }

    
    public void Dispose()
    {
        aiController = null;
        killTransform = null;
        
        targetKnowledge.Dispose();
        targetKnowledge = null;
        
        pathKnowledge.Dispose();
        pathKnowledge = null;
        
        attackKnowledge.Dispose();
        attackKnowledge = null;
    }
}
