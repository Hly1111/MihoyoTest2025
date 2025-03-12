using UnityEngine;

public class PlayerInAirState : PlayerState
{
    protected Vector3 ModifiedJumpForce
    {
        get
        {
            PlayerStateMachine.ReusableData.JumpForce.x = MovementInput == Vector2.zero? 0 : PlayerStateMachine.ReusableData.JumpModifier;
            PlayerStateMachine.ReusableData.JumpForce.z = MovementInput == Vector2.zero? 0 : PlayerStateMachine.ReusableData.JumpModifier;
            return PlayerStateMachine.ReusableData.JumpForce;
        }
    }
    
    protected PlayerInAirState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(PlayerStateMachine.Player.AnimationData.InAirParameter);
    }
    
    public override void Exit()
    {
        base.Exit();
        StopAnimation(PlayerStateMachine.Player.AnimationData.InAirParameter);
    }

    protected override void ContactGroundCallback(Collider collider)
    {
        PlayerStateMachine.ChangeState(PlayerStateMachine.JumpEndState);
    }
}
