using System.Collections;
using Core;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBlockState : PlayerReflectionState
{
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
                PlayerStateMachine.ReusableData.TargetEnemy = collider.gameObject.GetComponent<Projectile>().GetSource();
                PlayerStateMachine.ReusableData.TargetEnemy.EnemyAttackPerception.GetBlocked();
                PlayerStateMachine.Player.VfxDataHandler.PlayVfx(EVfxType.LighteningImpact);
                ObjectPool.Instance.ReturnObject(collider.gameObject.name, collider.gameObject);
                CommonMono.Instance.StartCoroutine(LimitTimeKillChanceCoroutine());
            }
            else
            {
                PlayerStateMachine.ReusableData.HitDirection = (collider.transform.position - PlayerStateMachine.Player.transform.position).normalized;
                PlayerStateMachine.ChangeState(PlayerStateMachine.HitState);
            }
        }
    }

    IEnumerator LimitTimeKillChanceCoroutine()
    {
        PlayerStateMachine.ReusableData.CanKill = true;
        yield return new WaitForSeconds(2);
        PlayerStateMachine.ReusableData.CanKill = false;
    }
}