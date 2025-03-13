using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Update Animation Event Type Here
/// </summary>
public enum EAnimNotify
{
    OnAttackOneStartInput,
    OnAttackOneEndInput,
    OnAttackTwoStartInput,
    OnAttackTwoEndInput,
    OnAttackThreeStartInput,
    OnAttackThreeEndInput,
    OnAttackOneEnd,
    OnAttackTwoEnd,
    OnAttackThreeEnd,
    OnJumpEndStartPreInput,
    OnJumpEndEnd,
    OnRunEndStartPreInput,
    OnRunEndEnd,
    OnHitEnd,
    OnBlockStart,
    OnBlockEnd
}

public class AnimEventData
{
    private UnityAction _attackCallback;
    
    public void AddAttackCallback(UnityAction callback)
    {
        _attackCallback += callback;
    }
    
    public void RemoveAttackCallback(UnityAction callback)
    {
        _attackCallback -= callback;
    }

    public void TriggerAttackCallback()
    {
        _attackCallback?.Invoke();
    }
}