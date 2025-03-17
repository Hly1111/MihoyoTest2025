using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerState : IState
{
    protected readonly PlayerStateMachine PlayerStateMachine;
    
    protected Vector2 MovementInput;

    private bool _isPreinputBinded;
    
    protected PlayerState(PlayerStateMachine playerStateMachine)
    {
        PlayerStateMachine = playerStateMachine;
    }
    
    #region animation
    protected virtual void StartAnimation(int animationHash)
    {
        PlayerStateMachine.Player.Animator.SetBool(animationHash, true);
    }
    
    protected virtual void StopAnimation(int animationHash)
    {
        PlayerStateMachine.Player.Animator.SetBool(animationHash, false);
    }
    #endregion
    
    #region reusable methods
    protected void HandleInput()
    {
        if(PlayerStateMachine.Player.PlayerInput.enabled == false)
            return;
        MovementInput = PlayerStateMachine.Player.PlayerInput.GameplayActions.Move.ReadValue<Vector2>();
    }
    
    protected Vector3 GetMovementInputDirection()
    {
        return new Vector3(MovementInput.x, 0, MovementInput.y).normalized;
    }
    
    protected void RotateTowardsTargetRotation()
    {
        float currentYAngle = PlayerStateMachine.Player.Rigidbody.rotation.eulerAngles.y;
        float targetYAngle = PlayerStateMachine.ReusableData.CurrentTargetRotation.y;
        float angleDifference = Mathf.DeltaAngle(currentYAngle, targetYAngle);
        if (Mathf.Abs(angleDifference) < 0.1f)
            return;
        float newYAngle = Mathf.SmoothDampAngle(currentYAngle, targetYAngle, ref PlayerStateMachine.ReusableData.DampedTargetRotationCurrentVelocity.y, PlayerStateMachine.ReusableData.TimeToReachTargetRotation.y);
        PlayerStateMachine.ReusableData.DampedTargetRotationSmoothTime.y += Time.deltaTime;
        Quaternion targetRotation = Quaternion.Euler(0, newYAngle, 0);
        
        PlayerStateMachine.Player.Rigidbody.MoveRotation(targetRotation);
    }
    
    protected Vector3 GetTargetRotateDirection(float directionAngle)
    {
        Vector3 direction = Quaternion.Euler(0, directionAngle, 0) * Vector3.forward;
        return direction;
    }
    
    protected float Rotate(Vector3 direction)
    {
        float directionAngle = UpdateTargetRotation(direction);

        RotateTowardsTargetRotation();
        return directionAngle;
    }

    protected float Rotate(Vector3 direction, bool shouldConsiderCameraRotation)
    {
        float directionAngle = UpdateTargetRotation(direction, shouldConsiderCameraRotation);

        RotateTowardsTargetRotation();
        return directionAngle;
    }
    
    protected float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotation = true)
    {
        float directionAngle = GetDirectionAngle(direction);
        if (shouldConsiderCameraRotation)
        {
            directionAngle = AddCameraRotationToAngle(directionAngle);
        }
        if (!Mathf.Approximately(directionAngle, PlayerStateMachine.ReusableData.CurrentTargetRotation.y))
        {
            UpdateTargetRotationData(directionAngle);
        }

        return directionAngle;
    }
    
    protected void ResetVelocity()
    {
        PlayerStateMachine.Player.Rigidbody.linearVelocity = Vector3.zero;
        PlayerStateMachine.Player.Rigidbody.angularVelocity = Vector3.zero;
    }
    
    protected void ResetVerticalVelocity()
    {
        PlayerStateMachine.Player.Rigidbody.linearVelocity = new Vector3(PlayerStateMachine.Player.Rigidbody.linearVelocity.x, 0 , PlayerStateMachine.Player.Rigidbody.linearVelocity.z);
        PlayerStateMachine.Player.Rigidbody.angularVelocity = Vector3.zero;
    }
    
    protected void ResetHorizontalVelocity()
    {
        PlayerStateMachine.Player.Rigidbody.linearVelocity = new Vector3(0, PlayerStateMachine.Player.Rigidbody.linearVelocity.y, 0);
        PlayerStateMachine.Player.Rigidbody.angularVelocity = Vector3.zero;
    }
    
    protected Vector3 GetPlayerVerticalVelocity()
    {
        return PlayerStateMachine.Player.Rigidbody.linearVelocity.y * Vector3.up;
    }
    
    protected void Float()
    {
        Vector3 heightInWorldSpace =
            PlayerStateMachine.Player.CapsuleColliderHandler.CapsuleColliderData.CapsuleCollider.bounds.center;
        Ray downwardRaycast = new Ray(heightInWorldSpace, Vector3.down);
        if(Physics.Raycast(downwardRaycast, out RaycastHit hit, PlayerStateMachine.ReusableData.DownRaycastDistance, PlayerStateMachine.Player.LayerUtility.GroundLayer, QueryTriggerInteraction.Ignore))
        {
            float distanceToFloatingPoint =
                PlayerStateMachine.Player.CapsuleColliderHandler.CapsuleColliderData.LocalSpaceCenter.y
                * PlayerStateMachine.Player.Rigidbody.gameObject.transform.localScale.y - hit.distance;
            if (Mathf.Approximately(distanceToFloatingPoint, 0))
            {
                return;
            }
            float targetVelocity = PlayerStateMachine.ReusableData.FloatForce * distanceToFloatingPoint - GetPlayerVerticalVelocity().y;
            targetVelocity = Mathf.Lerp(targetVelocity, PlayerStateMachine.ReusableData.FloatForce * distanceToFloatingPoint, Time.fixedDeltaTime / 0.05f);

            Vector3 liftForce = targetVelocity * Vector3.up;
            PlayerStateMachine.Player.Rigidbody.AddForce(liftForce, ForceMode.VelocityChange);
        }
    }
    
    #endregion
    
    private void UpdateTargetRotationData(float targetAngle)
    {
        PlayerStateMachine.ReusableData.CurrentTargetRotation.y = targetAngle;
        PlayerStateMachine.ReusableData.DampedTargetRotationSmoothTime.y = 0;
    }

    private float AddCameraRotationToAngle(float angle)
    {
        angle += PlayerStateMachine.Player.MainCamera.transform.eulerAngles.y;
        if (angle > 360f)
        {
            angle -= 360f;
        }

        return angle;
    }

    private float GetDirectionAngle(Vector3 direction)
    {
        float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        directionAngle = (directionAngle+360)%360;
        return directionAngle;
    }
    
    #region interface methods
    public virtual void Enter()
    {
    }

    public virtual void Update()
    {
        HandleInput();
    }

    public virtual void FixedUpdate()
    {
    }
    
    public virtual void Exit()
    {
    }

    public virtual void OnTriggerEnter(Collider collider)
    {
        if (PlayerStateMachine.Player.LayerUtility.IsGroundLayer(collider.gameObject.layer))
        {
            ContactGroundCallback(collider);
        }

        if (collider.CompareTag("Projectile"))
        {
            PlayerStateMachine.ReusableData.HitDirection = (collider.transform.position - PlayerStateMachine.Player.transform.position).normalized;
            PlayerStateMachine.ChangeState(PlayerStateMachine.HitState);
        }
    }
    
    public virtual void OnTriggerExit(Collider collider)
    {
        if (PlayerStateMachine.Player.LayerUtility.IsGroundLayer(collider.gameObject.layer))
        {
            LeaveGroundCallback(collider);
        }
    }
    #endregion

    #region collider callback methods
    
    protected virtual void ContactGroundCallback(Collider collider)
    {
        
    }
    
    protected virtual void LeaveGroundCallback(Collider collider)
    {
        
    }
    
    
    #endregion
    
    #region input callbacks
    
    protected virtual void AddInputCallbacks()
    {
        
    }
    
    protected virtual void RemoveInputCallbacks()
    {
        
    }

    protected void MoveCallback(InputAction.CallbackContext obj)
    {
        PlayerStateMachine.ChangeState(PlayerStateMachine.RunningState);
    }

    protected void JumpCallback(InputAction.CallbackContext obj)
    {
        PlayerStateMachine.ChangeState(PlayerStateMachine.JumpStartState);
    }

    protected void AttackCallback(InputAction.CallbackContext obj)
    {
        if (PlayerStateMachine.ReusableData.CanKill)
        {
            PlayerStateMachine.ChangeState(PlayerStateMachine.WaitForKillState);
        }
        else
        {
            PlayerStateMachine.ReusableData.TargetEnemy = PlayerStateMachine.Player.EnemySelectionHandler.GetClosestEnemy( PlayerStateMachine.Player.transform);
            PlayerStateMachine.ChangeState(PlayerStateMachine.AttackOneState);
        }
    }

    protected void ReflectCallback(InputAction.CallbackContext obj)
    {
        PlayerStateMachine.ChangeState(PlayerStateMachine.BlockState);
    }
    
    public void AddPreInputCallback()
    {
        if (!_isPreinputBinded)
        {
            if (PlayerStateMachine.Player.PlayerInput.GameplayActions.Move.IsPressed())
            {
                PlayerStateMachine.ChangeState(PlayerStateMachine.RunningState);
            }
            else if (PlayerStateMachine.Player.PlayerInput.GameplayActions.Jump.IsPressed())
            {
                PlayerStateMachine.ChangeState(PlayerStateMachine.JumpStartState);
            }
            else
            {
                PlayerStateMachine.Player.PlayerInput.GameplayActions.Move.performed += MoveCallback;
                PlayerStateMachine.Player.PlayerInput.GameplayActions.Jump.performed += JumpCallback;
            }
        }
        _isPreinputBinded = true;
    }

    public void RemovePreInputCallback()
    {
        if (_isPreinputBinded)
        {
            PlayerStateMachine.Player.PlayerInput.GameplayActions.Move.performed -= MoveCallback;
            PlayerStateMachine.Player.PlayerInput.GameplayActions.Jump.performed -= JumpCallback;
        }
        _isPreinputBinded = false;
    }
    #endregion
}
