using System;
using UnityEngine;

[Serializable]
public class EnemyAIData
{
    [field: SerializeField] public LayerMask DetectionLayer { get; private set; }
    [field: SerializeField] public float SightRange { get; private set; }
    [field: SerializeField] public float SightAngle { get; private set; }
    [field: SerializeField] public float PatrolRange { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    
    [field: SerializeField] public float AttackRate { get; private set; }
    [field: SerializeField] public float AttackDuration { get; private set; }
    
}
