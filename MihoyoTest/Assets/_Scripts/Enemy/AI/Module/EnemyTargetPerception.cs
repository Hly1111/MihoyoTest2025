

using UnityEngine;

public class EnemyTargetPerception : EnemyAIBrain
{
    protected override void InitializeInternal()
    {
        base.InitializeInternal();
        EnemyAIKnowledge.targetKnowledge.target =
            GameObject.FindWithTag("Player").transform.root.GetComponentInChildren<PlayerController>();
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
        EnemyAIKnowledge.targetKnowledge.targetDirection = EnemyAIKnowledge.targetKnowledge.target.transform.position - EnemyAIKnowledge.aiController.EnemyAIData.EyeTransform.position;
        Vector3 targetDirection = EnemyAIKnowledge.targetKnowledge.targetDirection;
        
        EnemyAIKnowledge.targetKnowledge.distance = EnemyAIKnowledge.targetKnowledge.targetDirection.magnitude;
        float distanceToTarget = EnemyAIKnowledge.targetKnowledge.distance;
        
        if (Vector3.Angle(enemyForward, targetDirection) <= sightAngle / 2)
        {
            if (distanceToTarget <= sightRange)
            {
                Vector3 rayOrigin = EnemyAIKnowledge.aiController.EnemyAIData.EyeTransform.position;
                Ray ray = new Ray(rayOrigin, targetDirection);
                if(Physics.Raycast(ray, out var hit,sightRange, targetLayer))
                {
                    Debug.DrawRay(rayOrigin, targetDirection, Color.green);
                    if (hit.collider.CompareTag("Player"))
                    {
                        Debug.DrawRay(rayOrigin, targetDirection, Color.green);
                        EnemyAIKnowledge.targetKnowledge.isTargetVisible = true;
                        return;
                    }
                }
            }
        }
        EnemyAIKnowledge.targetKnowledge.isTargetVisible = false;
    }
}