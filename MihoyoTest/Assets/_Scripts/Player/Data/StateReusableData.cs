using UnityEngine;

public class StateReusableData
{
    public float BaseSpeed { get; set; }
    public float SpeedModifier { get; set; }
    public Vector3 TimeToReachTargetRotation { get; set; }
    public float MovementDecelerationForce { get; set; }
    public float JumpModifier { get; set; }
    public float FallLimitSpeed { get; set; }
    public float AttackForce { get; set; }
    public int AttackIndex { get; set; }
    public float DownRaycastDistance { get; set; }
    public float FloatForce { get; set; }
    public Vector3 HitDirection { get; set; }
    public bool CanKill { get; set; }
    public EnemyAIController TargetEnemy { get; set; }


    public ref Vector3 JumpForce => ref _jumpForce;
    private Vector3 _jumpForce;
    
    public ref Vector3 CurrentTargetRotation => ref _currentTargetRotation;
    private Vector3 _currentTargetRotation;
    
    public ref Vector3 DampedTargetRotationSmoothTime => ref _dampedTargetRotationSmoothTime;
    private Vector3 _dampedTargetRotationSmoothTime;
    public ref Vector3 DampedTargetRotationCurrentVelocity  => ref _dampedTargetRotationCurrentVelocity;
    private Vector3 _dampedTargetRotationCurrentVelocity;

    public StateReusableData(PlayerDataSO playerDataSO)
    {
        BaseSpeed = playerDataSO.baseSpeed;
        SpeedModifier = playerDataSO.speedModifier;
        TimeToReachTargetRotation = playerDataSO.timeToReachTargetRotation;
        MovementDecelerationForce = playerDataSO.movementDecelerationForce;
        _jumpForce = playerDataSO.jumpForce;
        JumpModifier = playerDataSO.jumpModifier;
        FallLimitSpeed = playerDataSO.fallLimitSpeed;
        AttackForce = playerDataSO.attackForce;
        AttackIndex = 0;
        DownRaycastDistance = playerDataSO.downRaycastDistance;
        FloatForce = playerDataSO.floatForce;
        
        _currentTargetRotation = Vector3.zero;
        _dampedTargetRotationSmoothTime = Vector3.zero;
        _dampedTargetRotationCurrentVelocity = Vector3.zero;
        
        HitDirection = Vector3.zero;
        CanKill = false;
        
        TargetEnemy = null;
    }
}