using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        PlayerStateMachine.ReusableData.AttackIndex = 0;
        StartAnimation(PlayerStateMachine.Player.AnimationData.IdleParameter);
        AddInputCallbacks();
        
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
        RemoveInputCallbacks();
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
