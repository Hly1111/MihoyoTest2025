using UnityEngine;

public class StateReusableData
{
    private float _baseSpeed;
    private float _speedModifier;
    
    private Vector3 _timeToReachTargetRotation;
    private float _movementDecelerationForce;
    
    private Vector3 _jumpForce;
    private float _jumpModifier;
    private float _fallLimitSpeed;
    
    private float _attackForce;
    
    private float _downRaycastDistance;
    private float _floatForce;
    
    private Vector3 _currentTargetRotation;
    private Vector3 _dampedTargetRotationCurrentVelocity;
    private Vector3 _dampedTargetRotationSmoothTime;
    
    public StateReusableData(PlayerDataSO playerDataSO)
    {
        _baseSpeed = playerDataSO.baseSpeed;
        _speedModifier = playerDataSO.speedModifier;
        _timeToReachTargetRotation = playerDataSO.timeToReachTargetRotation;
        _movementDecelerationForce = playerDataSO.movementDecelerationForce;
        _jumpForce = playerDataSO.jumpForce;
        _jumpModifier = playerDataSO.jumpModifier;
        _fallLimitSpeed = playerDataSO.fallLimitSpeed;
        _attackForce = playerDataSO.attackForce;
        _downRaycastDistance = playerDataSO.downRaycastDistance;
        _floatForce = playerDataSO.floatForce;
        
        _currentTargetRotation = Vector3.zero;
        _dampedTargetRotationCurrentVelocity = Vector3.zero;
        _dampedTargetRotationSmoothTime = Vector3.zero;
    }
    
    public ref float BaseSpeed => ref _baseSpeed;
    public ref float SpeedModifier => ref _speedModifier;
    public ref Vector3 TimeToReachTargetRotation => ref _timeToReachTargetRotation;
    public ref float MovementDecelerationForce => ref _movementDecelerationForce;
    public ref Vector3 JumpForce => ref _jumpForce;
    public ref float JumpModifier => ref _jumpModifier;
    public ref float FallLimitSpeed => ref _fallLimitSpeed;
    public ref float AttackForce => ref _attackForce;
    public ref float DownRaycastDistance => ref _downRaycastDistance;
    public ref float FloatForce => ref _floatForce;
    
    public ref Vector3 CurrentTargetRotation => ref _currentTargetRotation;
    public ref Vector3 DampedTargetRotationCurrentVelocity => ref _dampedTargetRotationCurrentVelocity;
    public ref Vector3 DampedTargetRotationSmoothTime => ref _dampedTargetRotationSmoothTime;
}
