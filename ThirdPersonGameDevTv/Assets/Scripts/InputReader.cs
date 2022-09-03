using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public event Action JumpEvent;
    public event Action DodgeEvent;
    
    public Vector2 MovementValue { get; private set; }
    
    private Controls _controls;
    
    private void Start()
    {
        _controls = new Controls();
        _controls.Player.SetCallbacks(this); // Binds this object with player control action map.
        
        _controls.Player.Enable();
    }

    private void OnDisable()
    {
        _controls.Player.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        JumpEvent?.Invoke();
    }
    
    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        DodgeEvent?.Invoke();
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }
}
