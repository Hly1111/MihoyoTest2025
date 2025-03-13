using System;
using UnityEngine;

[Serializable]
public class EnemyAttackKnowledge : IDisposable
{
    [field: SerializeField] public bool canAttack;
    [field: SerializeField] public bool shouldAttack;
    
    
    public void Initialize()
    {
        canAttack = false;
        shouldAttack = false;
    }

    public void Dispose()
    {
        canAttack = false;
        shouldAttack = false;
    }
}