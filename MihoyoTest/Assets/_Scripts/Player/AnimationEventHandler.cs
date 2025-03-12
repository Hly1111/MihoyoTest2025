using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHandler : MonoBehaviour
{
    [SerializeField] 
    private readonly Dictionary<EAnimNotify, AnimEventData> _animEventDataDict = new Dictionary<EAnimNotify, AnimEventData>();
    
    public void InitializeData()
    {
        foreach (EAnimNotify animNotify in (EAnimNotify[])Enum.GetValues(typeof(EAnimNotify)))
        {
            _animEventDataDict[animNotify] = new AnimEventData();
        }
    }
    
    public void AddEventToData(EAnimNotify animNotify, UnityAction callback)
    {
        if(_animEventDataDict.TryGetValue(animNotify, out var value))
        {
            value.AddAttackCallback(callback);
        }
        else
        {
            Debug.LogWarning("AnimEventData not found");
        }
    }
    
    public void RemoveEventFromData(EAnimNotify animNotify, UnityAction callback)
    {
        if(_animEventDataDict.TryGetValue(animNotify, out var value))
        {
            value.RemoveAttackCallback(callback);
        }
        else
        {
            Debug.LogWarning("AnimEventData not found");
        }
    }

    public void AnimEvent_AttackNotify(EAnimNotify animNotify)
    {
        if(_animEventDataDict.TryGetValue(animNotify, out var value))
        {
            value.TriggerAttackCallback();
        }
        else
        {
            Debug.LogWarning("AnimEventData not found");
        }
    }
}
