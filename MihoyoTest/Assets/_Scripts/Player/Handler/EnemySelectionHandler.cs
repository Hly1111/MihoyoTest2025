using System;
using UnityEngine;

[Serializable]
public class EnemySelectionHandler
{
    [field: SerializeField] public float PlayerFOV { get; private set; }
    [field: SerializeField] public float SelectionRadius { get; private set; }
    [field: SerializeField] public LayerMask EnemyLayer { get; private set; }
    
    public EnemyAIController GetClosestEnemy(Transform playerTransform)
    {
        Collider[] hits  = Physics.OverlapSphere(playerTransform.position, SelectionRadius, EnemyLayer, QueryTriggerInteraction.Ignore);
        if(hits.Length == 0)
            return null;
        
        Transform closestEnemy = null;
        float minDistance = float.MaxValue;
        float minAngle = PlayerFOV;
        foreach (var hit in hits)
        {
            Transform enemyTransform = hit.transform;
            Vector3 direction = (enemyTransform.position - playerTransform.position).normalized;
            float distance = Vector3.Distance(enemyTransform.position, playerTransform.position);
            float angle = Vector3.Angle(playerTransform.forward, direction);
            if(angle <= minAngle && distance < minDistance)
            {
                minDistance = distance;
                minAngle = angle;
                closestEnemy = enemyTransform;
            }
            else if(Mathf.Approximately(distance, minDistance) && angle < minAngle)
            {
                minAngle = angle;
                closestEnemy = enemyTransform;
            }
        }
        if(closestEnemy != null)
            return closestEnemy.root.GetComponentInChildren<EnemyAIController>();
        return null;
    }
}