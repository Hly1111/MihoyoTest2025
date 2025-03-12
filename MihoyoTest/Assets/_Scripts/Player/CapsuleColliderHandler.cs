using System;
using UnityEngine;

[Serializable]
public class CapsuleColliderHandler
{
    public CapsuleColliderData CapsuleColliderData;
    
    [field: SerializeField] public CapsuleColliderDataSO CapsuleColliderDataSO { get; private set; }
    [field: SerializeField][field: Range(0f,1f)] public float SlopeData { get; private set; }

    public void Initialize(GameObject obj)
    {
        if(CapsuleColliderData!=null) return;
        CapsuleColliderData = new CapsuleColliderData();
        CapsuleColliderData.Initialize(obj);
    }

    public void CalculateCapsuleData()
    {
        SetRadius(CapsuleColliderDataSO.radius);
        SetHeight(CapsuleColliderDataSO.height * (1 - SlopeData));

        ResetCenter();
        
        if(CapsuleColliderData.CapsuleCollider.height/2f < CapsuleColliderData.CapsuleCollider.radius)
        {
            SetRadius(CapsuleColliderData.CapsuleCollider.height/2f);
        }
        
        CapsuleColliderData.UpdateCapsuleCenter();
    }

    private void ResetCenter()
    {
        float difference = CapsuleColliderDataSO.height - CapsuleColliderData.CapsuleCollider.height;
        Vector3 newCenter = new Vector3(0, CapsuleColliderDataSO.centerY + 0.5f * difference, 0);
        CapsuleColliderData.CapsuleCollider.center = newCenter;
    }

    private void SetRadius(float radius)
    {
        CapsuleColliderData.CapsuleCollider.radius = radius;
    }

    private void SetHeight(float height)
    {
        CapsuleColliderData.CapsuleCollider.height = height;
    }
}
