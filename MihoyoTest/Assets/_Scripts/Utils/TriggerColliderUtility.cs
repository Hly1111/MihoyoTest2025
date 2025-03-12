
using UnityEngine;

[System.Serializable]
public class TriggerColliderUtility
{
    [field: SerializeField] public Collider Collider { get; private set; }
    public Vector3 GroundCheckColliderVerticalExtents { get; private set; }
    
    public void Initialize()
    {
        GroundCheckColliderVerticalExtents = Collider.bounds.extents;
    }
}
