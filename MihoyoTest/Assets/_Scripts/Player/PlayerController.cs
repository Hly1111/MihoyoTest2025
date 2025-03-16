using System;
using Core;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour, IController
{
    public Camera MainCamera { get; private set; }
    public PlayerInput PlayerInput { get; private set; }
    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerAnimEventHandler PlayerAnimEventHandler { get; private set; }
    
    [field: SerializeField] public AnimationData AnimationData { get; private set; }
    [field: SerializeField] public CapsuleColliderHandler CapsuleColliderHandler { get; private set; }
    [field: SerializeField] public TriggerColliderUtility TriggerColliderUtility { get; private set; }
    [field: SerializeField] public LayerUtility LayerUtility { get; private set; }
    [field: SerializeField] public PlayerDataSO PlayerData { get; private set; }

    [field: SerializeField] public VfxDataHandler VfxDataHandler { get; private set; }
    
    private PlayerStateMachine _stateMachine;

    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();
        Rigidbody = transform.root.GetComponentInChildren<Rigidbody>();
        Animator = transform.root.GetComponentInChildren<Animator>();
        PlayerAnimEventHandler = transform.root.GetComponentInChildren<PlayerAnimEventHandler>();
        MainCamera = Camera.main;
        
        _stateMachine = new PlayerStateMachine(this);
        AnimationData.Initialize(Animator);
        CapsuleColliderHandler.Initialize(gameObject.transform.root.gameObject);
        CapsuleColliderHandler.CalculateCapsuleData();
        TriggerColliderUtility.Initialize();
    }

    private void OnValidate()
    {
        CapsuleColliderHandler.Initialize(gameObject.transform.root.gameObject);
        CapsuleColliderHandler.CalculateCapsuleData();
    }

    private void Start()
    {
        _stateMachine.ChangeState(_stateMachine.IdleState);
        UIManager.Instance.ShowPanel<VisualizerPanel>("VisualizerPanel");
    }
    
    private void Update()
    {
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    public void OnTriggerEnter(Collider other)
    {
        _stateMachine.OnTriggerEnter(other);
    }
    
    public void OnTriggerExit(Collider other)
    {
        _stateMachine.OnTriggerExit(other);
    }
}
