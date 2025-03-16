using System;
using UnityEngine;

[Serializable]
public class EnemyAttackKnowledge : IDisposable
{
    [field: SerializeField] public bool canAttack;
    [field: SerializeField] public bool shouldAttack;
    [field: SerializeField] public float timer;
    
    
    public void Initialize()
    {
        canAttack = false;
        shouldAttack = false;
        timer = 0;
    }

    public void Dispose()
    {
        canAttack = false;
        shouldAttack = false;
        timer = 0;
    }
}