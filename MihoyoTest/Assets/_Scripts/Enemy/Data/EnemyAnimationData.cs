using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class EnemyAnimationData
{
    [SerializeField] private string groundedParameter = "Grounded";
    [SerializeField] private string moveParameter = "Move";
    
    [SerializeField] private string idleParameter = "IsIdle";
    [SerializeField] private string runParameter = "IsRun";
    [SerializeField] private string attackParameter = "IsAttack";
    [SerializeField] private string dieParameter = "IsDie";
    [SerializeField] private string hurtParameter = "IsHurt";
    
    public int GroundedParameter { get; private set; }
    public int MoveParameter { get; private set; }
    
    public int IdleParameter { get; private set; }
    public int RunParameter { get; private set; }
    public int AttackParameter { get; private set; }
    public int DieParameter { get; private set; }
    public int HurtParameter { get; private set; }
    
    public void Initialize(Animator animator)
    {
        GroundedParameter = Animator.StringToHash(groundedParameter);
        MoveParameter = Animator.StringToHash(moveParameter);
        
        IdleParameter = Animator.StringToHash(idleParameter);
        RunParameter = Animator.StringToHash(runParameter);
        AttackParameter = Animator.StringToHash(attackParameter);
        DieParameter = Animator.StringToHash(dieParameter);
        HurtParameter = Animator.StringToHash(hurtParameter);
    }
}