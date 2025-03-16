using UnityEngine;
using UnityEngine.Events;
public class AnimEventData
{
    private UnityAction _callback;
    
    public void AddCallback(UnityAction callback)
    {
        _callback += callback;
    }
    
    public void RemoveCallback(UnityAction callback)
    {
        _callback -= callback;
    }

    public void TriggerCallback()
    {
        _callback?.Invoke();
    }
}