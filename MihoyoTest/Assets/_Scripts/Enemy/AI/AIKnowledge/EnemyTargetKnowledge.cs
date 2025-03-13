
using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class EnemyTargetKnowledge : IDisposable
{
    [field: SerializeField] public PlayerController target;
    [field: SerializeField] public Vector3 targetDirection;
    [field: SerializeField] public float distance;
    [field: SerializeField] public bool isTargetVisible;
    
    public void Initialize()
    {
        target = null;
        targetDirection = Vector3.zero;
        distance = targetDirection.magnitude;
        isTargetVisible = false;
    }
    
    public void Dispose()
    {
        target = null;
        targetDirection = Vector3.zero;
        distance = 0;
        isTargetVisible = false;
    }
}
