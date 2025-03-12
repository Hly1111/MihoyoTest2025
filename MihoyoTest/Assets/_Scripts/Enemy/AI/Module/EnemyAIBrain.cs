using UnityEngine;

public abstract class EnemyAIBrain
{
    protected EnemyAIKnowledge EnemyAIKnowledge { get; private set; }

    public void Initialize(EnemyAIKnowledge enemyAIKnowledge)
    {
        EnemyAIKnowledge = enemyAIKnowledge;
        InitializeInternal();
    }

    protected virtual void InitializeInternal()
    {
        
    }

    public void UpdateDecision()
    {
        UpdateDecisionInternal();
    }
    
    protected virtual void UpdateDecisionInternal()
    {
        
    }

    public void Clear()
    {
        
    }
    
    protected virtual void ClearInternal()
    {
        EnemyAIKnowledge.Dispose();   
    }
}
