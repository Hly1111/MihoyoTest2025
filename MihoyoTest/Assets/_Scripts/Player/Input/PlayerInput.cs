using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerInputActions InputActions { get; private set; }
    public PlayerInputActions.GameplayActions GameplayActions { get; private set; }

    private void Awake()
    {
        InputActions = new PlayerInputActions();
        GameplayActions = InputActions.Gameplay;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void OnEnable()
    {
        InputActions.Enable();
    }
    
    private void OnDisable()
    {
        InputActions.Disable();
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
