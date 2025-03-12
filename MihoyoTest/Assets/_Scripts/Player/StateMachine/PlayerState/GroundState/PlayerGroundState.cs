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

    private bool IsGroundUnderneath()
    {
        TriggerColliderUtility triggerColliderUtility = PlayerStateMachine.Player.TriggerColliderUtility;
        Vector3 groundColliderCenterInWorldSpace = triggerColliderUtility.Collider.bounds.center;
        Collider[] overlappedGroundColliders = Physics.OverlapBox(groundColliderCenterInWorldSpace, triggerColliderUtility.GroundCheckColliderVerticalExtents, 
            triggerColliderUtility.Collider.transform.rotation, PlayerStateMachine.Player.LayerUtility.GroundLayer, QueryTriggerInteraction.Ignore);
        return overlappedGroundColliders.Length > 0;
    }

    private void Float()
    {
        Vector3 heightInWorldSpace =
            PlayerStateMachine.Player.CapsuleColliderHandler.CapsuleColliderData.CapsuleCollider.bounds.center;
        Ray downwardRaycast = new Ray(heightInWorldSpace, Vector3.down);
        if(Physics.Raycast(downwardRaycast, out RaycastHit hit, PlayerStateMachine.ReusableData.DownRaycastDistance, PlayerStateMachine.Player.LayerUtility.GroundLayer, QueryTriggerInteraction.Ignore))
        {
            float distanceToFloatingPoint =
                PlayerStateMachine.Player.CapsuleColliderHandler.CapsuleColliderData.LocalSpaceCenter.y
                * PlayerStateMachine.Player.Rigidbody.gameObject.transform.localScale.y - hit.distance;
            if (Mathf.Approximately(distanceToFloatingPoint, 0))
            {
                return;
            }
            float targetVelocity = PlayerStateMachine.ReusableData.FloatForce * distanceToFloatingPoint - GetPlayerVerticalVelocity().y;
            targetVelocity = Mathf.Lerp(targetVelocity, PlayerStateMachine.ReusableData.FloatForce * distanceToFloatingPoint, Time.fixedDeltaTime / 0.05f);

            Vector3 liftForce = targetVelocity * Vector3.up;
            PlayerStateMachine.Player.Rigidbody.AddForce(liftForce, ForceMode.VelocityChange);
        }
    }
    
    protected Vector3 GetHorizontalVelocity()
    {
        return PlayerStateMachine.Player.Rigidbody.linearVelocity;
    }
}
