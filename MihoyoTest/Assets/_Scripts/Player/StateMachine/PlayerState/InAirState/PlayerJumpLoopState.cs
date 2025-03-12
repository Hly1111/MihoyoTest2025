using UnityEngine;

public class PlayerJumpLoopState : PlayerInAirState
{
    public PlayerJumpLoopState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
        
    }
    
    public override void Enter()
    {
        base.Enter();
        PlayerStateMachine.ReusableData.SpeedModifier = 0f;
        ResetVerticalVelocity();
        
        StartAnimation(PlayerStateMachine.Player.AnimationData.JumpLoopParameter);
    }
    
    public override void Exit()
    {
        base.Exit();
        StopAnimation(PlayerStateMachine.Player.AnimationData.JumpLoopParameter);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Vector3 playerVerticalVelocity = GetPlayerVerticalVelocity();
        if (playerVerticalVelocity.y >= -PlayerStateMachine.ReusableData.FallLimitSpeed)
        {
            return;
        }
        Vector3 limitedVelocityForce = new Vector3(0f, -PlayerStateMachine.ReusableData.FallLimitSpeed - playerVerticalVelocity.y, 0f);
        PlayerStateMachine.Player.Rigidbody.AddForce(limitedVelocityForce, ForceMode.VelocityChange);
    }
}
