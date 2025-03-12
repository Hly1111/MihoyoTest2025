using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataSO", menuName = "PlayerData", order = 0)]
public class PlayerDataSO : ScriptableObject
{
    [Header("Movement")]
    public float baseSpeed = 5f;
    public float speedModifier = 1f;
    public Vector3 timeToReachTargetRotation = new Vector3(0f, 0.14f, 0f);
    public float movementDecelerationForce = 0.5f;

    [Header("Jump")] 
    public Vector3 jumpForce = new Vector3(0f, 5f, 0f);
    public float jumpModifier = 1f;
    public Vector3 inAirTimeToReachTargetRotation = new Vector3(0f, 0.02f, 0f);
    public float fallLimitSpeed = 10f;
    
    [Header("Attack")]
    public float attackForce = 3f;

    [Header("Capsule Float")] 
    public float downRaycastDistance = 2f;
    public float floatForce = 25f;
}
