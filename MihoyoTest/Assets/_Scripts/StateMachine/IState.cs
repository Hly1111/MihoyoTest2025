using UnityEngine;

public interface IState
{
    public void Enter();
    public void Update();
    public void FixedUpdate();
    public void Exit();

    public void OnTriggerEnter(Collider collider);
    public void OnTriggerExit(Collider collider);
}
