using System;
using UnityEngine;

[Serializable]
public class EnemyAIData
{
    [field: SerializeField] public Transform EyeTransform { get; private set; }
    [field: SerializeField] public Transform ProjectileSpawnPoint { get; private set; }
    [field: SerializeField] public LayerMask DetectionLayer { get; private set; }
    [field: SerializeField] public float SightRange { get; private set; }
    [field: SerializeField] public float SightAngle { get; private set; }
    [field: SerializeField] public float PatrolRange { get; private set; }
    
    [field: SerializeField] public float MaxAttackDuration { get; private set; }
    [field: SerializeField] public float MinAttackDuration { get; private set; }
    
}
