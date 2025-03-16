using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public enum EAnimEvent
{
    
}

public abstract class AnimEventHandler<T> : MonoBehaviour where T : Enum
{
    private readonly Dictionary<T, AnimEventData> _animEventDataDict = new Dictionary<T, AnimEventData>();

    protected virtual void InitializeData()
    {
        foreach (T animNotify in (T[])Enum.GetValues(typeof(T)))
        {
            _animEventDataDict[animNotify] = new AnimEventData();
        }
    }

    protected virtual void AddEventToData(T animNotify, UnityAction callback)
    {
        if(_animEventDataDict.TryGetValue(animNotify, out var value))
        {
            value.AddCallback(callback);
        }
        else
        {
            Debug.LogWarning("AnimEventData not found");
        }
    }

    protected virtual void RemoveEventFromData(T animNotify, UnityAction callback)
    {
        if(_animEventDataDict.TryGetValue(animNotify, out var value))
        {
            value.RemoveCallback(callback);
        }
        else
        {
            Debug.LogWarning("AnimEventData not found");
        }
    }

    public virtual void TriggerEvent(T animNotify)
    {
        if(_animEventDataDict.TryGetValue(animNotify, out var value))
        {
            value.TriggerCallback();
        }
        else
        {
            Debug.LogWarning("AnimEventData not found");
        }
    }
    
    public void BindAllAnimEvents(StateMachine caller)
    {
        Type callerType = caller.GetType();
        InitializeData();
        foreach (T notify in Enum.GetValues(typeof(T)))
        {
            string methodName = $"AnimEvent_{notify}";
            MethodInfo method = callerType.GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
            if (method != null)
            {
                UnityAction eventDelegate = (UnityAction)Delegate.CreateDelegate(typeof(UnityAction), caller, method);
                AddEventToData(notify, eventDelegate);
            }
            else
            {
                Debug.LogWarning("Fail to bound " + methodName);
            }
        }
    }
    
    public void UnbindAllAnimEvents(StateMachine caller) 
    {
        Type callerType = caller.GetType();
        foreach (T notify in Enum.GetValues(typeof(T)))
        {
            string methodName = $"AnimEvent_{notify}";
            MethodInfo method = callerType.GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
            if (method != null)
            {
                UnityAction eventDelegate = (UnityAction)Delegate.CreateDelegate(typeof(UnityAction), caller, method);
                RemoveEventFromData(notify, eventDelegate);
            }
        }
    }
}