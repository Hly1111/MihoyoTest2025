using UnityEngine;

public interface IController
{
    public void OnTriggerEnter(Collider collider);
    
    public void OnTriggerExit(Collider collider);
}
