using UnityEngine;

public class PlayerHitState : PlayerState
{
    public PlayerHitState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        ResetHorizontalVelocity();
        StartAnimation(PlayerStateMachine.Player.AnimationData.HitParameter);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Vector3 targetDirection = -new Vector3(PlayerStateMachine.ReusableData.HitDirection.x, 0, PlayerStateMachine.ReusableData.HitDirection.z);
        UpdateTargetRotation(CalculateHitDirection() == 1 ? -targetDirection : targetDirection, false);
        RotateTowardsTargetRotation();
        Float();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(PlayerStateMachine.Player.AnimationData.HitParameter);
    }

    protected override void StartAnimation(int animationHash)
    {
        base.StartAnimation(animationHash);
        PlayerStateMachine.Player.Animator.SetFloat(PlayerStateMachine.Player.AnimationData.HitDirectionParameter, CalculateHitDirection());
    }

    private int CalculateHitDirection()
    {
        Vector3 hitDirection = PlayerStateMachine.ReusableData.HitDirection;
        Vector3 playerDirection = PlayerStateMachine.Player.transform.forward;
        float angle = Vector3.Angle(hitDirection, playerDirection);
        if (angle >= 90)
        {
            return -1;
        }
        return 1;
    }
}