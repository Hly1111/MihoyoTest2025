
using System;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class EnemyPathKnowledge : IDisposable
{
    [field: SerializeField] public NavMeshAgent NavMeshAgent;

    public void Initialize(NavMeshAgent navMeshAgent)
    {
        NavMeshAgent = navMeshAgent;
    }
    
    public void Dispose()
    {
        NavMeshAgent = null;
    }
    
    public bool IsStopped => NavMeshAgent.isStopped;
    
    public Vector3 Destination => NavMeshAgent.destination;
    
    public float RemainingDistance => NavMeshAgent.remainingDistance;
    
    public bool HasPath => NavMeshAgent.hasPath;
    
    public bool PathPending => NavMeshAgent.pathPending;
    
    public Vector3 Velocity => NavMeshAgent.velocity;
}
