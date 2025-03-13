using System;
using System.Collections;
using UnityEngine;

public class EnemyAIController : MonoBehaviour, IController, IPoolObject
{
    private EnemyStateMachine _enemyStateMachine;
    public readonly EnemyPathPerception EnemyPathPerception = new EnemyPathPerception();
    public readonly EnemyTargetPerception EnemyTargetPerception = new EnemyTargetPerception();
    
    public Animator Animator { get; private set; }
    [field: SerializeField] public EnemyAIKnowledge EnemyAIKnowledge{ get; private set; }
    [field: SerializeField] public EnemyAIData EnemyAIData { get; private set; }
    [field: SerializeField] public EnemyAnimationData EnemyAnimationData { get; private set; }
    
    private void Awake()
    {
        _enemyStateMachine = new EnemyStateMachine(this);
        
        Animator = transform.root.GetComponentInChildren<Animator>();
        EnemyAnimationData.Initialize(Animator);
    }

    private void Start()
    {
        Initialize();
        _enemyStateMachine.ChangeState(_enemyStateMachine.EnemyIdleState);
    }

    public void OnActivate()
    {
        Initialize();
    }

    public void OnDeactivate()
    {
        EnemyAIKnowledge.Dispose();
        EnemyAIKnowledge = null;
        
        EnemyPathPerception.Clear();
        EnemyTargetPerception.Clear();
    }

    private void Initialize()
    {
        EnemyAIKnowledge = new EnemyAIKnowledge();
        EnemyAIKnowledge.Initialize(this);
        EnemyPathPerception.Initialize(EnemyAIKnowledge);
        EnemyTargetPerception.Initialize(EnemyAIKnowledge);
    }

    private void Update()
    {
        _enemyStateMachine.Update();
    }

    private void FixedUpdate()
    {
        _enemyStateMachine.FixedUpdate();
    }

    public void OnTriggerEnter(Collider other)
    {
        _enemyStateMachine.OnTriggerEnter(other);
    }

    public void OnTriggerExit(Collider other)
    {
        _enemyStateMachine.OnTriggerExit(other);
    }


    #region Debug
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, EnemyAIData.SightRange);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, EnemyAIData.AttackRange);
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, EnemyAIData.PatrolRange);
        
        Gizmos.color = Color.yellow;
        Vector3 sightLine1 = Quaternion.Euler(0, EnemyAIData.SightAngle/2, 0) * transform.forward;
        Vector3 sightLine2 = Quaternion.Euler(0, -EnemyAIData.SightAngle/2, 0) * transform.forward;
        Gizmos.DrawLine(transform.position, transform.position + sightLine1.normalized * EnemyAIData.SightRange);
        Gizmos.DrawLine(transform.position, transform.position + sightLine2.normalized * EnemyAIData.SightRange);
    }

    public IEnumerator SendBackToPool(string objName, float timeToWait, GameObject obj)
    {
        yield return null;
    }
    #endregion
}
