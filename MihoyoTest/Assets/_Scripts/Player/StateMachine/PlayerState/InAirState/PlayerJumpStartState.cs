using UnityEngine;

public class PlayerJumpStartState : PlayerInAirState
{
    private bool _shouldRotate;
    private bool _canFall;
    public PlayerJumpStartState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(PlayerStateMachine.Player.AnimationData.JumpStartParameter);

        PlayerStateMachine.ReusableData.SpeedModifier = 0f;
        PlayerStateMachine.ReusableData.TimeToReachTargetRotation =
            PlayerStateMachine.Player.PlayerData.inAirTimeToReachTargetRotation;
        _shouldRotate = MovementInput != Vector2.zero;
        
        Jump();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(PlayerStateMachine.Player.AnimationData.JumpStartParameter);
        PlayerStateMachine.ReusableData.TimeToReachTargetRotation =
            PlayerStateMachine.Player.PlayerData.timeToReachTargetRotation;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (_shouldRotate)
        {
            RotateTowardsTargetRotation();
        }

        if (_canFall)
        {
            Vector3 playerVerticalVelocity = GetPlayerVerticalVelocity();

            PlayerStateMachine.Player.Rigidbody.AddForce(
                -playerVerticalVelocity * PlayerStateMachine.ReusableData.MovementDecelerationForce, ForceMode.Acceleration);
        }
    }

    public override void Update()
    {
        base.Update();

        if (!_canFall && GetPlayerVerticalVelocity().y > 0)
        {
            _canFall = true;
        }
        if(!_canFall || GetPlayerVerticalVelocity().y > 0)
        {
            return;
        }
        
        PlayerStateMachine.ChangeState(PlayerStateMachine.JumpLoopState);
    }

    private void Jump()
    {
        Vector3 jumpForce = ModifiedJumpForce;
        
        Vector3 currentDirection = PlayerStateMachine.Player.Rigidbody.transform.forward;

        if (_shouldRotate)
        {
            currentDirection = GetTargetRotateDirection(PlayerStateMachine.ReusableData.CurrentTargetRotation.y);
        }
        
        jumpForce.x *= currentDirection.x;
        jumpForce.z *= currentDirection.z;
        
        ResetVelocity();
        
        PlayerStateMachine.Player.Rigidbody.AddForce(jumpForce, ForceMode.VelocityChange);
    }
}
