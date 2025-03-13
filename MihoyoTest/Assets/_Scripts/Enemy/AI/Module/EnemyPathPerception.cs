using UnityEngine;
using UnityEngine.AI;

public class EnemyPathPerception : EnemyAIBrain
{
    protected override void InitializeInternal()
    {
        base.InitializeInternal();
        EnemyAIKnowledge.pathKnowledge.canMove = true;
    }

    protected override void UpdateDecisionInternal()
    {
        base.UpdateDecisionInternal(); 
        HandleMoveCondition();
        HandlePathFinding();
    }

    private void HandleMoveCondition()
    {
        if (EnemyAIKnowledge.pathKnowledge.canMove)
        {
            EnemyAIKnowledge.pathKnowledge.shouldMove = EnemyAIKnowledge.targetKnowledge.isTargetVisible;
            if (EnemyAIKnowledge.targetKnowledge.isTargetVisible)
            {
                if(EnemyAIKnowledge.targetKnowledge.distance <= EnemyAIKnowledge.aiController.EnemyAIData.PatrolRange)
                {
                    EnemyAIKnowledge.pathKnowledge.shouldMove = false;
                }
            }
        }
        else
        {
            EnemyAIKnowledge.pathKnowledge.shouldMove = false;
        }
    }

    private void HandlePathFinding()
    {
        Vector3 targetDirection = EnemyAIKnowledge.targetKnowledge.targetDirection;
        Vector3 selfPosition = EnemyAIKnowledge.aiController.transform.position;
        Vector3 targetDestination = selfPosition + targetDirection;
        
        if(EnemyAIKnowledge.targetKnowledge.isTargetVisible)
        {
            if (EnemyAIKnowledge.pathKnowledge.shouldMove)
            {
                EnemyAIKnowledge.pathKnowledge.canMove = SetDestination(new Vector3(targetDestination.x, 0, targetDestination.z));
                
                Vector3 velocity = EnemyAIKnowledge.pathKnowledge.Velocity;
                Vector3 frameTargetDestination = selfPosition + velocity.normalized;
                EnemyAIKnowledge.pathKnowledge.canMove = CanReachPath(frameTargetDestination);
                
                if(EnemyAIKnowledge.pathKnowledge.canMove)
                    Debug.DrawLine(selfPosition, targetDestination, Color.red);
            }
            else
            {
                StopMovement();
                SetFaceDirection(targetDirection);
            }
        }
        else
        {
            ResetPath();
        }
    }

    private bool SetDestination(Vector3 destination)
    {
        ResumeMovement();
        return EnemyAIKnowledge.pathKnowledge.navMeshAgent.SetDestination(destination);
    }
    
    private void SetFaceDirection(Vector3 direction)
    {
        EnemyAIKnowledge.aiController.transform.root.forward = Vector3.Lerp(EnemyAIKnowledge.aiController.transform.root.forward, direction, 20 * Time.deltaTime);
    }

    private void StopMovement()
    {
        EnemyAIKnowledge.pathKnowledge.navMeshAgent.isStopped = true;
    }

    private void ResumeMovement()
    {
        EnemyAIKnowledge.pathKnowledge.navMeshAgent.isStopped = false;
    }

    private void SetVelocity(Vector3 velocity)
    {
        EnemyAIKnowledge.pathKnowledge.navMeshAgent.velocity = velocity;
    }

    private void ResetPath()
    {
        EnemyAIKnowledge.pathKnowledge.navMeshAgent.ResetPath();
    }

    private bool CanReachPath(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();
        EnemyAIKnowledge.pathKnowledge.navMeshAgent.CalculatePath(targetPosition, path);
        return path.status == NavMeshPathStatus.PathComplete;
    }
}