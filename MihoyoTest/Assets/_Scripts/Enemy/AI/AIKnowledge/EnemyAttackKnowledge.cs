using System;
using UnityEngine;

[Serializable]
public class EnemyAttackKnowledge : IDisposable
{
    [field: SerializeField] public bool canAttack;
    [field: SerializeField] public bool shouldAttack;
    [field: SerializeField] public bool getBlocked;
    [field: SerializeField] public bool inKillState;
    [field: SerializeField] public float timer;
    
    
    public void Initialize()
    {
        canAttack = false;
        shouldAttack = false;
        getBlocked = false;
        inKillState = false;
        timer = 0;
    }

    public void Dispose()
    {
        canAttack = false;
        shouldAttack = false;
        getBlocked = false;
        inKillState = false;
        timer = 0;
    }
}