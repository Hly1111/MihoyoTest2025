
using UnityEngine;

public class EnemyState : IState
{
    protected EnemyStateMachine EnemyStateMachine;
    
    protected EnemyState(EnemyStateMachine enemyStateMachine)
    {
        EnemyStateMachine = enemyStateMachine;
    }
    
    public virtual void Enter()
    {
        
    }

    public virtual void Update()
    {
        
    }

    public virtual void FixedUpdate()
    {
        
    }

    public virtual void Exit()
    {
        
    }

    public virtual void OnTriggerEnter(Collider collider)
    {
        
    }

    public virtual void OnTriggerExit(Collider collider)
    {
        
    }
    
    protected virtual void StartAnimation(int animationParameter)
    {
        EnemyStateMachine.EnemyAIController.Animator.SetBool(animationParameter,true);
    }
    
    protected virtual void StopAnimation(int animationParameter)
    {
        EnemyStateMachine.EnemyAIController.Animator.SetBool(animationParameter,false);
    }
}
