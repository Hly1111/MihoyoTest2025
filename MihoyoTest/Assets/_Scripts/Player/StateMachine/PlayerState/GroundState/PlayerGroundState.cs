using UnityEngine;

public class PlayerGroundState : PlayerState
{
    protected PlayerGroundState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        StartAnimation(PlayerStateMachine.Player.AnimationData.GroundParameter);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(PlayerStateMachine.Player.AnimationData.GroundParameter);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Float();
    }

    protected override void LeaveGroundCallback(Collider collider)
    {
        base.LeaveGroundCallback(collider);
        if (IsGroundUnderneath())
        {
            return;
        }
        Vector3 capsuleColliderCenterInWorldSpace = PlayerStateMachine.Player.CapsuleColliderHandler.CapsuleColliderData.CapsuleCollider.bounds.center;
        Ray downwardsRayFromCapsuleBottom = new Ray(capsuleColliderCenterInWorldSpace - 
                                                    PlayerStateMachine.Player.CapsuleColliderHandler.CapsuleColliderData.ColliderVerticalExtents, Vector3.down);
        if (!Physics.Raycast(downwardsRayFromCapsuleBottom, out _, 1, PlayerStateMachine.Player.LayerUtility.GroundLayer, QueryTriggerInteraction.Ignore))
        {
            PlayerStateMachine.ChangeState(PlayerStateMachine.JumpLoopState);
        }
    }

    protected bool IsGroundUnderneath()
    {
        TriggerColliderUtility triggerColliderUtility = PlayerStateMachine.Player.TriggerColliderUtility;
        Vector3 groundColliderCenterInWorldSpace = triggerColliderUtility.Collider.bounds.center;
        Collider[] overlappedGroundColliders = Physics.OverlapBox(groundColliderCenterInWorldSpace, triggerColliderUtility.GroundCheckColliderVerticalExtents, 
            triggerColliderUtility.Collider.transform.rotation, PlayerStateMachine.Player.LayerUtility.GroundLayer, QueryTriggerInteraction.Ignore);
        return overlappedGroundColliders.Length > 0;
    }
    
    protected Vector3 GetHorizontalVelocity()
    {
        return PlayerStateMachine.Player.Rigidbody.linearVelocity;
    }
    
    #region input callbacks
    protected override void AddInputCallbacks()
    {
        base.AddInputCallbacks();
        PlayerStateMachine.Player.PlayerInput.GameplayActions.Attack.performed += AttackCallback;

        PlayerStateMachine.Player.PlayerInput.GameplayActions.Jump.performed += JumpCallback;
        
        PlayerStateMachine.Player.PlayerInput.GameplayActions.Reflect.performed += ReflectCallback;
    }

    protected override void RemoveInputCallbacks()
    {
        base.RemoveInputCallbacks();
        PlayerStateMachine.Player.PlayerInput.GameplayActions.Attack.performed -= AttackCallback;
        
        PlayerStateMachine.Player.PlayerInput.GameplayActions.Jump.performed -= JumpCallback;
        
        PlayerStateMachine.Player.PlayerInput.GameplayActions.Reflect.performed -= ReflectCallback;
    }
    
    #endregion
}
