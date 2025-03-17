using UnityEngine;

public class CameraShakeHandler : MonoBehaviour
{
        public void ShakeCamera(float force)
        {
                CameraManager.Instance.ShakeCamera(force);
        }
}