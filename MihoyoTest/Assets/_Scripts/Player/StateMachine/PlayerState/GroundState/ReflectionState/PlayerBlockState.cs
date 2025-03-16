using Core;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBlockState : PlayerReflectionState
{
    public bool IsBlocked;
    public bool CanBlock;
    
    public PlayerBlockState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        ResetVelocity();
        StartAnimation(PlayerStateMachine.Player.AnimationData.BlockParameter);
    }
    
    public override void Exit()
    {
        base.Exit();
        StopAnimation(PlayerStateMachine.Player.AnimationData.BlockParameter);
    }
    
    public override void OnTriggerEnter(Collider collider)
    {
        if (PlayerStateMachine.Player.LayerUtility.IsGroundLayer(collider.gameObject.layer))
        {
            ContactGroundCallback(collider);
        }
        
        if (collider.CompareTag("Projectile"))
        {
            PlayerStateMachine.ReusableData.HitDirection = (collider.transform.position - PlayerStateMachine.Player.transform.position).normalized;
            if (CanBlock)
            {
                // ObjectPool.Instance.GetObject("LightningImpact", (vfx) =>
                // {
                //     vfx.transform.position = PlayerStateMachine.Player.VfxDataHandler.LighteningImpactVfx.VfxSpawnPoint.position;
                //     vfx.transform.rotation = PlayerStateMachine.Player.VfxDataHandler.LighteningImpactVfx.VfxSpawnPoint.rotation;
                // });
                // AudioManager.Instance.PlaySfx("shoot_lightning");
                PlayerStateMachine.Player.VfxDataHandler.PlayVfx(EVfxType.LighteningImpact);
                ObjectPool.Instance.ReturnObject(collider.gameObject.name, collider.gameObject);
                PlayerStateMachine.ChangeState(PlayerStateMachine.WaitForKillState);
            }
            else
            {
                PlayerStateMachine.ReusableData.HitDirection = (collider.transform.position - PlayerStateMachine.Player.transform.position).normalized;
                PlayerStateMachine.ChangeState(PlayerStateMachine.HitState);
            }
        }
    }
}