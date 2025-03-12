

using UnityEngine;

public class EnemyTargetUpdater : EnemyAIBrain
{
    protected override void UpdateDecisionInternal()
    {
        base.UpdateDecisionInternal();
        HandleDetection();
    }
    
    private void HandleDetection()
    {
        float sightAngle = EnemyAIKnowledge.AIController.EnemyAIData.SightAngle;
        float sightRange = EnemyAIKnowledge.AIController.EnemyAIData.SightRange;
        LayerMask targetLayer = EnemyAIKnowledge.AIController.EnemyAIData.DetectionLayer;
        
        Vector3 enemyForward = EnemyAIKnowledge.AIController.transform.forward;
        EnemyAIKnowledge.TargetKnowledge.TargetDirection = EnemyAIKnowledge.AIController.Target.GetTransform().position - EnemyAIKnowledge.AIController.transform.position;
        Vector3 targetDirection = EnemyAIKnowledge.TargetKnowledge.TargetDirection;
        
        EnemyAIKnowledge.TargetKnowledge.Distance = EnemyAIKnowledge.TargetKnowledge.TargetDirection.magnitude;
        float distanceToTarget = EnemyAIKnowledge.TargetKnowledge.Distance;
        
        if (2 * Mathf.Abs(Vector3.Angle(enemyForward, targetDirection)) <= sightAngle)
        {
            if (distanceToTarget <= sightRange)
            {
                Ray ray = new Ray(EnemyAIKnowledge.AIController.transform.position, targetDirection);
                if(Physics.Raycast(ray, out var hit, sightRange, targetLayer))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        EnemyAIKnowledge.TargetKnowledge.IsTargetVisible = true;
                    }
                    else
                    {
                        EnemyAIKnowledge.TargetKnowledge.IsTargetVisible = false;
                    }
                }
            }
        }
    }
}