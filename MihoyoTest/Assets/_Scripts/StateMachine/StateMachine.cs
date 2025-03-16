using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public abstract class StateMachine
{
    protected IState CurrentState;
    public IState CurrentStateType => CurrentState;
    
    public void ChangeState(IState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
    
    public void Update()
    {
        CurrentState?.Update();
    }
    
    public void FixedUpdate()
    {
        CurrentState?.FixedUpdate();
    }
    
    public void OnTriggerEnter(Collider collider)
    {
        CurrentState?.OnTriggerEnter(collider);
    }
    
    public void OnTriggerExit(Collider collider)
    {
        CurrentState?.OnTriggerExit(collider);
    }
}
