using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerStateMachine : StateMachine
{
    public PlayerController Player { get; }
    public StateReusableData ReusableData { get; }
    
    public PlayerIdleState IdleState { get; }
    public PlayerRunningState RunningState { get; }
    public PlayerRunEndState RunEndState { get; }
    
    public PlayerJumpStartState JumpStartState { get; }
    public PlayerJumpLoopState JumpLoopState { get; }
    public PlayerJumpEndState JumpEndState { get; }
    
    public PlayerAttackOneState AttackOneState { get; }
    public PlayerAttackTwoState AttackTwoState { get; }
    public PlayerAttackHeavyState AttackHeavyState { get; }
    
    public PlayerHitState HitState { get; }
    
    public PlayerBlockState BlockState { get; }
    public PlayerKillState KillState { get; }

    public PlayerStateMachine(PlayerController player)
    {
        Player = player;
        ReusableData = new StateReusableData(player.PlayerData);
        
        IdleState = new PlayerIdleState(this);
        RunningState = new PlayerRunningState(this);
        RunEndState = new PlayerRunEndState(this);
        
        JumpStartState = new PlayerJumpStartState(this);
        JumpLoopState = new PlayerJumpLoopState(this);
        JumpEndState = new PlayerJumpEndState(this);
        
        AttackOneState = new PlayerAttackOneState(this);
        AttackTwoState = new PlayerAttackTwoState(this);
        AttackHeavyState = new PlayerAttackHeavyState(this);
        
        HitState = new PlayerHitState(this);
        
        BlockState = new PlayerBlockState(this);
        KillState = new PlayerKillState(this);
        
        BindAllAnimEvents();
    }

    private void BindAllAnimEvents()
    {
        Player.AnimationEventHandler.InitializeData();
        foreach (EAnimNotify notify in Enum.GetValues(typeof(EAnimNotify)))
        {
            string methodName = $"AnimEvent_{notify}";
            MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
            if (method != null)
            {
                UnityAction eventDelegate = (UnityAction)Delegate.CreateDelegate(typeof(UnityAction), this, method);
                Player.AnimationEventHandler.AddEventToData(notify, eventDelegate);
            }
            else
            {
                Debug.LogWarning("Fail to bound " + methodName);
            }
        }
    }
    
    private void UnbindAllAnimEvents()
    {
        foreach (EAnimNotify notify in Enum.GetValues(typeof(EAnimNotify)))
        {
            string methodName = $"AnimEvent_{notify}";
            MethodInfo method = GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
            if (method != null)
            {
                UnityAction eventDelegate = (UnityAction)Delegate.CreateDelegate(typeof(UnityAction), this, method);
                Player.AnimationEventHandler.RemoveEventFromData(notify, eventDelegate);
            }
        }
    }
    
    #region AttackOneState
    private void AnimEvent_OnAttackOneStartInput()
    {
        Player.PlayerInput.GameplayActions.Attack.performed += AttackOneState.AttackDoneCallback;
    }

    private void AnimEvent_OnAttackOneEndInput()
    {
        Player.PlayerInput.GameplayActions.Attack.performed -= AttackOneState.AttackDoneCallback;
        if (AttackOneState.HasAttack)
        {
            ChangeState(AttackTwoState);
        }
    }
    
    private void AnimEvent_OnAttackOneEnd()
    {
        if(!AttackOneState.HasAttack)
            ChangeState(IdleState);
    }
    #endregion
    
    #region AttackTwoState
    
    private void AnimEvent_OnAttackTwoStartInput()
    {
        Player.PlayerInput.GameplayActions.Attack.performed += AttackTwoState.AttackDoneCallback;
    }
    
    private void AnimEvent_OnAttackTwoEndInput()
    {
        Player.PlayerInput.GameplayActions.Attack.performed -= AttackTwoState.AttackDoneCallback;
        if (AttackTwoState.HasAttack)
        {
            ChangeState(AttackHeavyState);
        }
    }
    
    private void AnimEvent_OnAttackTwoEnd()
    {
        if(!AttackTwoState.HasAttack)
            ChangeState(IdleState);
    }
    
    #endregion
    
    #region AttackHeavyState
    
    private void AnimEvent_OnAttackThreeStartInput()
    {
        Player.PlayerInput.GameplayActions.Attack.performed += AttackHeavyState.AttackDoneCallback;
    }
    
    private void AnimEvent_OnAttackThreeEndInput()
    {
        Player.PlayerInput.GameplayActions.Attack.performed -= AttackHeavyState.AttackDoneCallback;
        if (AttackHeavyState.HasAttack)
        {
            ChangeState(AttackOneState);
        }
        AttackHeavyState.AddPreInputCallback();
    }
    
    private void AnimEvent_OnAttackThreeEnd()
    {
        if(!AttackHeavyState.HasAttack)
            ChangeState(IdleState);
    }
    
    #endregion
    
    #region RunEndState
    
    private void AnimEvent_OnRunEndStartPreInput()
    {
        RunEndState.AddPreInputCallback();
    }
    
    private void AnimEvent_OnRunEndEnd()
    {
        RunEndState.RemovePreInputCallback();
        ChangeState(IdleState);
    }
    
    #endregion
    
    #region HitState
    
    private void AnimEvent_OnHitEnd()
    {
        ChangeState(IdleState);
    }
    
    #endregion
    
    #region JumpEndState
    
    private void AnimEvent_OnJumpEndStartPreInput()
    {
        JumpEndState.AddPreInputCallback();
    }
    
    private void AnimEvent_OnJumpEndEnd()
    {
        ChangeState(IdleState);
    }
    
    #endregion
    
    #region BlockState
    
    private void AnimEvent_OnBlockStart()
    {
        
    }
    
    private void AnimEvent_OnBlockEnd()
    {
        ChangeState(IdleState);
    }
    
    #endregion
}
