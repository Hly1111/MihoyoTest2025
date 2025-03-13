

using UnityEngine;

public class EnemyTargetPerception : EnemyAIBrain
{
    protected override void InitializeInternal()
    {
        base.InitializeInternal();
        EnemyAIKnowledge.targetKnowledge.target =
            GameObject.FindWithTag("Player").GetComponentInChildren<PlayerController>();
    }

    protected override void UpdateDecisionInternal()
    {
        base.UpdateDecisionInternal();
        HandleDetection();
    }
    
    private void HandleDetection()
    {
        float sightAngle = EnemyAIKnowledge.aiController.EnemyAIData.SightAngle;
        float sightRange = EnemyAIKnowledge.aiController.EnemyAIData.SightRange;
        LayerMask targetLayer = EnemyAIKnowledge.aiController.EnemyAIData.DetectionLayer;
        
        Vector3 enemyForward = EnemyAIKnowledge.aiController.transform.forward;
        EnemyAIKnowledge.targetKnowledge.targetDirection = EnemyAIKnowledge.targetKnowledge.target.transform.position - EnemyAIKnowledge.aiController.transform.position;
        Vector3 targetDirection = EnemyAIKnowledge.targetKnowledge.targetDirection;
        
        EnemyAIKnowledge.targetKnowledge.distance = EnemyAIKnowledge.targetKnowledge.targetDirection.magnitude;
        float distanceToTarget = EnemyAIKnowledge.targetKnowledge.distance;
        
        if (2 * Mathf.Abs(Vector3.Angle(enemyForward, targetDirection)) <= sightAngle)
        {
            if (distanceToTarget <= sightRange)
            {
                Ray ray = new Ray(EnemyAIKnowledge.aiController.transform.position + Vector3.up, targetDirection);
                if(Physics.SphereCast(ray, 5, out var hit,sightRange, targetLayer, QueryTriggerInteraction.Ignore))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        EnemyAIKnowledge.targetKnowledge.isTargetVisible = true;
                        return;
                    }
                }
            }
        }
        EnemyAIKnowledge.targetKnowledge.isTargetVisible = false;
    }
}