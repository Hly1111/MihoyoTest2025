using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class AnimationData
{
    [SerializeField] private string groundParameter = "Ground";
    [SerializeField] private string movementParameter = "Movement";
    [SerializeField] private string attackParameter = "Attack";
    [SerializeField] private string inAirParameter = "InAir";
    
    [SerializeField] private string idleParameter = "IsIdle";
    [SerializeField] private string runningParameter = "IsRunning";
    [SerializeField] private string runEndParameter = "IsRunEnd";
    
    [SerializeField] private string attackOneParameter = "IsAttack1";
    [SerializeField] private string attackTwoParameter = "IsAttack2";
    [SerializeField] private string attackHeavyParameter = "IsAttackHeavy";
    
    [SerializeField] private string jumpStartParameter = "IsJumpStart";
    [SerializeField] private string jumpLoopParameter = "IsJumpLoop";
    [SerializeField] private string jumpEndParameter = "IsJumpEnd";
    
    public int GroundParameter { get; private set; }
    public int MovementParameter { get; private set; }
    public int AttackParameter { get; private set; }
    public int InAirParameter { get; private set; }
    
    public int IdleParameter { get; private set; }
    public int RunningParameter { get; private set; }
    public int RunEndParameter { get; private set; }
    
    public int AttackOneParameter { get; private set; }
    public int AttackTwoParameter { get; private set; }
    public int AttackHeavyParameter { get; private set; }
    
    public int JumpStartParameter { get; private set; }
    public int JumpLoopParameter { get; private set; }
    public int JumpEndParameter { get; private set; }
    
    public void Initialize(Animator animator)
    {
        GroundParameter = Animator.StringToHash(groundParameter);
        MovementParameter = Animator.StringToHash(movementParameter);
        AttackParameter = Animator.StringToHash(attackParameter);
        InAirParameter = Animator.StringToHash(inAirParameter);
        
        IdleParameter = Animator.StringToHash(idleParameter);
        RunningParameter = Animator.StringToHash(runningParameter);
        RunEndParameter = Animator.StringToHash(runEndParameter);
        AttackOneParameter = Animator.StringToHash(attackOneParameter);
        AttackTwoParameter = Animator.StringToHash(attackTwoParameter);
        AttackHeavyParameter = Animator.StringToHash(attackHeavyParameter);
        
        JumpStartParameter = Animator.StringToHash(jumpStartParameter);
        JumpLoopParameter = Animator.StringToHash(jumpLoopParameter);
        JumpEndParameter = Animator.StringToHash(jumpEndParameter);
    }
}
