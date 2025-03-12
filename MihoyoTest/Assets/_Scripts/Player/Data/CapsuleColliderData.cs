using System;
using UnityEngine;

public class CapsuleColliderData
{
    public CapsuleCollider CapsuleCollider { get; private set; }
    public Vector3 LocalSpaceCenter { get; private set; }
    public Vector3 ColliderVerticalExtents { get; private set; }

    public void Initialize(GameObject gameObject)
    {
        if(CapsuleCollider !=null) return;
        CapsuleCollider = gameObject.GetComponent<CapsuleCollider>();
        
        UpdateCapsuleCenter();
    }

    public void UpdateCapsuleCenter()
    {
        LocalSpaceCenter = CapsuleCollider.center;
        ColliderVerticalExtents = new Vector3(0, CapsuleCollider.bounds.extents.y, 0);
    }
}
