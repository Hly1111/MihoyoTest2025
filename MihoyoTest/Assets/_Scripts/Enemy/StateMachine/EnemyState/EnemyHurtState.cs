using UnityEngine;

public class EnemyHurtState: EnemyState
{
    public EnemyHurtState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        EnemyStateMachine.EnemyAIController.VfxDataHandler.PlayVfx(EVfxType.EnemyHurt);
        StartAnimation(EnemyStateMachine.EnemyAIController.EnemyAnimationData.HurtParameter);
    }
    
    public override void Exit()
    {
        base.Exit();
        StopAnimation(EnemyStateMachine.EnemyAIController.EnemyAnimationData.HurtParameter);
    }
    
    public override void OnTriggerEnter(Collider collider)
    {
        base.OnTriggerEnter(collider);
        if(collider.CompareTag("PlayerAttack"))
        {
            GetHit();
        }
    }

    private void GetHit()
    {
        EnemyStateMachine.ChangeState(EnemyStateMachine.EnemyHurtState);
    }
}