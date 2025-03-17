using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementState : PlayerGroundState
{
    protected PlayerMovementState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    
    #region interface
    public override void Enter()
    {
        base.Enter();
        AddInputCallbacks();
        StartAnimation(PlayerStateMachine.Player.AnimationData.MovementParameter);
    }
    
    public override void Exit()
    {
        base.Exit();
        RemoveInputCallbacks();
        StopAnimation(PlayerStateMachine.Player.AnimationData.MovementParameter);
    }
    
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        HandleMovement();
    }
    
    #endregion
    
    private void HandleMovement()
    {
        if (MovementInput == Vector2.zero || PlayerStateMachine.ReusableData.SpeedModifier == 0)
            return;
        Vector3 moveDirection = GetMovementInputDirection();
        float moveSpeed = GetMoveSpeed();
        float directionAngle = Rotate(moveDirection);
        var direction = GetTargetRotateDirection(directionAngle);
        Vector3 currentVelocity = GetHorizontalVelocity();
        PlayerStateMachine.Player.Rigidbody.AddForce(moveSpeed * direction - currentVelocity, ForceMode.VelocityChange);
    }

    private float GetMoveSpeed()
    {
        return PlayerStateMachine.ReusableData.BaseSpeed * PlayerStateMachine.ReusableData.SpeedModifier;
    }
}
