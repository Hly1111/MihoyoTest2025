using System;
using System.Collections;
using Core;
using UnityEngine;
using Object = System.Object;

public class EnemyAIController : MonoBehaviour, IController, IPoolObject
{
    private EnemyStateMachine _enemyStateMachine;
    public readonly EnemyPathPerception EnemyPathPerception = new EnemyPathPerception();
    public readonly EnemyTargetPerception EnemyTargetPerception = new EnemyTargetPerception();
    public readonly EnemyAttackPerception EnemyAttackPerception = new EnemyAttackPerception();
    
    public Animator Animator { get; private set; }
    public EnemyAnimEventHandler EnemyAnimEventHandler { get; private set; }
    [field: SerializeField] public EnemyAIKnowledge EnemyAIKnowledge{ get; private set; }
    [field: SerializeField] public EnemyAIData EnemyAIData { get; private set; }
    [field: SerializeField] public EnemyAnimationData EnemyAnimationData { get; private set; }
    [field: SerializeField] public GameObject ProjectilePrefab { get; private set; }
    [field: SerializeField] public VfxDataHandler VfxDataHandler { get; private set; }
    
    private void Awake()
    {
        Animator = transform.root.GetComponentInChildren<Animator>();
        EnemyAnimEventHandler = transform.root.GetComponentInChildren<EnemyAnimEventHandler>();
        
        _enemyStateMachine = new EnemyStateMachine(this);
        EnemyAnimationData.Initialize(Animator);
    }

    private void Start()
    {
        Initialize();
        _enemyStateMachine.ChangeState(_enemyStateMachine.EnemyIdleState);
        
        // ObjectPool.Instance.GetObject("Projectile", (vfx) =>
        // {
        //     ObjectPool.Instance.ReturnObject("Projectile", vfx);
        // });
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
        EnemyAttackPerception.Clear();
    }

    private void Initialize()
    {
        EnemyAIKnowledge = new EnemyAIKnowledge();
        EnemyAIKnowledge.Initialize(this);
        EnemyPathPerception.Initialize(EnemyAIKnowledge);
        EnemyTargetPerception.Initialize(EnemyAIKnowledge);
        EnemyAttackPerception.Initialize(EnemyAIKnowledge);
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
    
    public void SetKillState()
    {
        EnemyAIKnowledge.attackKnowledge.inKillState = true;
    }

    public void GetKilled()
    {
        _enemyStateMachine.ChangeState(_enemyStateMachine.EnemyDieState);
    }
    
    #region Debug
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, EnemyAIData.SightRange);
        
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
