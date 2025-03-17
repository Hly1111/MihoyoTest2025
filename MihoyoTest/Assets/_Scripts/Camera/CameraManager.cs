using Core;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    [SerializeField] private CinemachineStateDrivenCamera finisherCamera;
    [SerializeField] private CinemachineImpulseSource impulseSource;

    public void ActivateStateCamera()
    {
        if(finisherCamera != null)
            finisherCamera.Priority = 20;
    }
    
    public void DeactivateStateCamera()
    {
        if (finisherCamera != null)
            finisherCamera.Priority = 0;
    }

    public void ShakeCamera(float force)
    {
        if (impulseSource != null)
            impulseSource.GenerateImpulse(force);
    }
}
