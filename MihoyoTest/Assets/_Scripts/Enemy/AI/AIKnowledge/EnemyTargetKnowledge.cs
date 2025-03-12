
using System;
using UnityEngine;

[Serializable]
public class EnemyTargetKnowledge : IDisposable
{
    [field: SerializeField] public Vector3 TargetDirection;
    [field: SerializeField] public float Distance;
    [field: SerializeField] public bool IsTargetVisible;
    
    public void Initialize()
    {
        TargetDirection = Vector3.zero;
        Distance = TargetDirection.magnitude;
        IsTargetVisible = false;
    }
    
    public void Dispose()
    {
        TargetDirection = Vector3.zero;
        Distance = 0;
        IsTargetVisible = false;
    }
}
