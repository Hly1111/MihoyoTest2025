using UnityEngine;

public class EnemyPathUpdater : EnemyAIBrain
{
    protected override void UpdateDecisionInternal()
    {
        base.UpdateDecisionInternal(); 
        HandleMoveCondition();
        HandlePathFinding();
    }

    private void HandleMoveCondition()
    {
        EnemyAIKnowledge.ShouldMove = EnemyAIKnowledge.TargetKnowledge.IsTargetVisible;
        if (EnemyAIKnowledge.TargetKnowledge.IsTargetVisible)
        {
            if(EnemyAIKnowledge.TargetKnowledge.Distance <= EnemyAIKnowledge.AIController.EnemyAIData.PatrolRange)
            {
                EnemyAIKnowledge.ShouldMove = false;
            }
        }
    }

    private void HandlePathFinding()
    {
        if(EnemyAIKnowledge.ShouldMove)
        {
            if (EnemyAIKnowledge.TargetKnowledge.IsTargetVisible)
            {
                Vector3 targetDirection = EnemyAIKnowledge.TargetKnowledge.TargetDirection;
                Vector3 selfPosition = EnemyAIKnowledge.AIController.transform.position;
                Vector3 targetDestination = selfPosition + targetDirection;
                SetDestination(new Vector3(targetDestination.x, 0, targetDestination.z));
                Debug.DrawLine(selfPosition, targetDestination, Color.red);
            }
            else
            {
                ResetPath();
            }
        }
        else
        {
            StopMovement();
        }
    }

    public void SetDestination(Vector3 destination)
    {
        ResumeMovement();
        EnemyAIKnowledge.PathKnowledge.NavMeshAgent.SetDestination(destination);
    }
    
    public void StopMovement()
    {
        EnemyAIKnowledge.PathKnowledge.NavMeshAgent.isStopped = true;
    }
    
    public void ResumeMovement()
    {
        EnemyAIKnowledge.PathKnowledge.NavMeshAgent.isStopped = false;
    }
    
    public void SetVelocity(Vector3 velocity)
    {
        EnemyAIKnowledge.PathKnowledge.NavMeshAgent.velocity = velocity;
    }
    
    public void ResetPath()
    {
        EnemyAIKnowledge.PathKnowledge.NavMeshAgent.ResetPath();
    }
}