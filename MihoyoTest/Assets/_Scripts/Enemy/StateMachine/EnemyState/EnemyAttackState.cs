using Core;
using UnityEngine;

public class EnemyAttackState: EnemyState
{
    private Projectile _projectile;
    private Vector3 _direction;
    
    public EnemyAttackState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        StartAnimation(EnemyStateMachine.EnemyAIController.EnemyAnimationData.AttackParameter);
        EnemyStateMachine.EnemyAIController.EnemyAttackPerception.ResetAttack();
        ObjectPool.Instance.GetObject("Projectile", ObjectPoolCallback);
    }
    
    public override void Exit()
    {
        base.Exit();
        StopAnimation(EnemyStateMachine.EnemyAIController.EnemyAnimationData.AttackParameter);
    }

    public void LaunchProjectile()
    {
        _projectile?.Launch(_direction);
        
        // Vector3 startPosition = EnemyStateMachine.EnemyAIController.EnemyAIData.ProjectileSpawnPoint.position;
        // Vector3 targetPosition = EnemyStateMachine.EnemyAIController.EnemyAIKnowledge.targetKnowledge.target.transform.position;
        // Vector3 direction = (targetPosition - startPosition).normalized;
        //
        // var projectile = Object.Instantiate(EnemyStateMachine.EnemyAIController.ProjectilePrefab, startPosition, Quaternion.LookRotation(direction)).GetComponent<Projectile>();
        // projectile.SetSource(EnemyStateMachine.EnemyAIController);
        // projectile.Launch(direction);
        // CommonMono.Instance.StartCoroutine(projectile.SendBackToPool(projectile.gameObject.name, 3, projectile.gameObject));
    }
    
    private void ObjectPoolCallback(GameObject vfx)
    {
        Vector3 startPosition = EnemyStateMachine.EnemyAIController.EnemyAIData.ProjectileSpawnPoint.position;
        Vector3 targetPosition = EnemyStateMachine.EnemyAIController.EnemyAIKnowledge.targetKnowledge.target.transform.position;
        _direction = targetPosition - startPosition;
        vfx.transform.position = startPosition;
        vfx.transform.LookAt(targetPosition);
        var projectile = vfx.GetComponent<Projectile>();
        projectile.SetSource(EnemyStateMachine.EnemyAIController);
        _projectile = projectile;
    }
}