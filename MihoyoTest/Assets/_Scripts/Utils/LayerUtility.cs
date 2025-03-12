using System;
using UnityEngine;

[Serializable]
public class LayerUtility
{
    [field: SerializeField] public LayerMask GroundLayer
    {
        get;
        private set;
    }
    
    public bool ContainLayer(LayerMask layerMask, int layer)
    {
        return (1 << layer & layerMask) != 0;
    }
    
    public bool IsGroundLayer(int layer)
    {
        return ContainLayer(GroundLayer, layer);
    }
}
