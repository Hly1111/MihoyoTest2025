using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRunningState : PlayerMovementState
{
    public PlayerRunningState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        StartAnimation(PlayerStateMachine.Player.AnimationData.RunningParameter);

        PlayerStateMachine.ReusableData.SpeedModifier = 1f;
    }
    
    public override void Exit()
    {
        base.Exit();
        StopAnimation(PlayerStateMachine.Player.AnimationData.RunningParameter);
    }

    protected override void AddInputCallbacks()
    {
        base.AddInputCallbacks();
        PlayerStateMachine.Player.PlayerInput.GameplayActions.Move.canceled += OnMoveCanceled;
    }
    
    protected override void RemoveInputCallbacks()
    {
        base.RemoveInputCallbacks();
        PlayerStateMachine.Player.PlayerInput.GameplayActions.Move.canceled -= OnMoveCanceled;
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        PlayerStateMachine.ChangeState(PlayerStateMachine.RunEndState);
    }
}
