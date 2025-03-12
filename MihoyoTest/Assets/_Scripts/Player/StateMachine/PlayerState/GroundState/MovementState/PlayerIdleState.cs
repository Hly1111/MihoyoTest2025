using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerIdleState : PlayerMovementState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(PlayerStateMachine.Player.AnimationData.IdleParameter);
        
        PlayerStateMachine.ReusableData.SpeedModifier = 0f;
        ResetVelocity();
        
        if (PlayerStateMachine.Player.PlayerInput.GameplayActions.Move.IsPressed())
        {
            PlayerStateMachine.ChangeState(PlayerStateMachine.RunningState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(PlayerStateMachine.Player.AnimationData.IdleParameter);
    }

    protected override void AddInputCallbacks()
    {
        base.AddInputCallbacks();
        PlayerStateMachine.Player.PlayerInput.GameplayActions.Move.performed += MoveCallback;
    }

    protected override void RemoveInputCallbacks()
    {
        base.RemoveInputCallbacks();
        PlayerStateMachine.Player.PlayerInput.GameplayActions.Move.performed -= MoveCallback;
    }
}
