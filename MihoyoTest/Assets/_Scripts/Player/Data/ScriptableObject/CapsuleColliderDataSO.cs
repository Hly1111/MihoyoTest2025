using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "CapsuleColliderDataSO", menuName = "CapsuleColliderDataSO", order = 0)]
public class CapsuleColliderDataSO : ScriptableObject
{
    public float height = 1.54f;
    public float radius = 0.2f;
    public float centerY = 0.77f;
}
