
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[Serializable]
public class EnemyPathKnowledge : IDisposable
{
    [field: SerializeField] public NavMeshAgent navMeshAgent;
    [field: SerializeField] public bool shouldMove;
    [field: SerializeField] public bool canMove;

    public void Initialize(NavMeshAgent agent)
    {
        navMeshAgent = agent;
        shouldMove = false;
        canMove = false;
    }
    
    public void Dispose()
    {
        navMeshAgent = null;
        shouldMove = false;
        canMove = false;
    }
    
    public bool IsStopped => navMeshAgent.isStopped;
    
    public Vector3 Destination => navMeshAgent.destination;
    
    public float RemainingDistance => navMeshAgent.remainingDistance;
    
    public bool HasPath => navMeshAgent.hasPath;
    
    public bool PathPending => navMeshAgent.pathPending;
    
    public Vector3 Velocity => navMeshAgent.velocity;
}
