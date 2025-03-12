using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerState : IState
{
    protected readonly PlayerStateMachine PlayerStateMachine;
    
    protected Vector2 MovementInput;
    
    protected PlayerState(PlayerStateMachine playerStateMachine)
    {
        PlayerStateMachine = playerStateMachine;
    }
    
    # region input callbacks
    protected virtual void AddInputCallbacks()
    {
        
    }
    
    protected virtual void RemoveInputCallbacks()
    {
        
    }

    protected virtual void AddPreInputCallback()
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
    
    protected virtual void RemovePreInputCallback()
    {
        PlayerStateMachine.Player.PlayerInput.GameplayActions.Move.performed -= MoveCallback;
        PlayerStateMachine.Player.PlayerInput.GameplayActions.Jump.performed -= JumpCallback;
    }
    
    #endregion 
    
    #region animation
    protected virtual void StartAnimation(int animationHash)
    {
        PlayerStateMachine.Player.Animator.SetBool(animationHash, true);
    }
    
    protected virtual void StopAnimation(int animationHash)
    {
        PlayerStateMachine.Player.Animator.SetBool(animationHash, false);
    }
    
    protected virtual void AddAnimEvent(EAnimNotify animNotifyType, UnityAction animEvent)
    {
        PlayerStateMachine.Player.AnimationEventHandler.AddEventToData(animNotifyType, animEvent);
    }
    
    protected virtual void RemoveAnimEvent(EAnimNotify animNotifyType, UnityAction animEvent)
    {
        PlayerStateMachine.Player.AnimationEventHandler.RemoveEventFromData(animNotifyType, animEvent);
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
        if(Mathf.Approximately(currentYAngle, PlayerStateMachine.ReusableData.CurrentTargetRotation.y))
            return;
        float newYAngle = Mathf.SmoothDampAngle(currentYAngle, PlayerStateMachine.ReusableData.CurrentTargetRotation.y, ref PlayerStateMachine.ReusableData.DampedTargetRotationCurrentVelocity.y, PlayerStateMachine.ReusableData.TimeToReachTargetRotation.y);
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
    
    protected Vector3 GetPlayerVerticalVelocity()
    {
        return PlayerStateMachine.Player.Rigidbody.linearVelocity.y * Vector3.up;
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
    }
    
    public virtual void OnTriggerExit(Collider collider)
    {
        if (PlayerStateMachine.Player.LayerUtility.IsGroundLayer(collider.gameObject.layer))
        {
            LeaveGroundCallback(collider);
        }
    }

    #region callback methods
    protected void MoveCallback(InputAction.CallbackContext context)
    {
        PlayerStateMachine.ChangeState(PlayerStateMachine.RunningState);
    }
    
    protected void JumpCallback(InputAction.CallbackContext context)
    {
        PlayerStateMachine.ChangeState(PlayerStateMachine.JumpStartState);
    }
    
    protected virtual void ContactGroundCallback(Collider collider)
    {
        
    }
    
    protected virtual void LeaveGroundCallback(Collider collider)
    {
        
    }
    #endregion
}
