using UnityEngine;

public interface IController
{
    public Transform GetTransform();
    
    public void OnTriggerEnter(Collider collider);
    
    public void OnTriggerExit(Collider collider);
}
