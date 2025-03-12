using UnityEngine;

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
    
    #region events
    protected override void AddInputCallbacks()
    {
        base.AddInputCallbacks();
        PlayerStateMachine.Player.PlayerInput.GameplayActions.Attack.performed += AttackCallback;

        PlayerStateMachine.Player.PlayerInput.GameplayActions.Jump.performed += JumpCallback;
    }
    
    protected override void RemoveInputCallbacks()
    {
        base.RemoveInputCallbacks();
        PlayerStateMachine.Player.PlayerInput.GameplayActions.Attack.performed -= AttackCallback;
        
        PlayerStateMachine.Player.PlayerInput.GameplayActions.Jump.performed -= JumpCallback;
    }
    
    protected virtual void AttackCallback(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        PlayerStateMachine.ChangeState(PlayerStateMachine.AttackOneState);
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

    protected float GetMoveSpeed()
    {
        return PlayerStateMachine.ReusableData.BaseSpeed * PlayerStateMachine.ReusableData.SpeedModifier;
    }
}
