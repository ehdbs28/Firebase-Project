using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/Input Control")]
public class InputControl : ScriptableObject, Controls.IPlayerActions, Controls.IUIActions
{
    public Vector3 MouseAimPosition { get; private set; }

    public Vector3 MouseWorldPosition
    {
        get
        {
            var worldPos = GameManager.Instance.MainCam.ScreenToWorldPoint(MouseAimPosition);
            worldPos.z = 0;
            return worldPos;
        }
    }
    
    // UI input event
    public event Action OnMenuUpEvent = null;
    public event Action OnMenuDownEvent = null;
    public event Action OnSelectEvent = null;
    public event Action OnBackEvent = null;
    public event Action OnAnyKeyEvent = null;

    private Controls _inputControls;
    
    private void OnEnable()
    {
        if (_inputControls == null)
        {
            _inputControls = new Controls();
            _inputControls.Player.SetCallbacks(this);
            _inputControls.UI.SetCallbacks(this);
        }
        
        _inputControls.Player.Enable();
        _inputControls.UI.Enable();
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        MouseAimPosition = context.ReadValue<Vector2>();
    }

    public void OnMenuUp(InputAction.CallbackContext context)
    {
        if(context.performed)   
        {
            OnMenuUpEvent?.Invoke();
        }
    }

    public void OnMenuDown(InputAction.CallbackContext context)
    {
        if(context.performed)   
        {
            OnMenuDownEvent?.Invoke();
        }
    }

    public void OnSelect(InputAction.CallbackContext context)
    {
        if(context.performed)   
        {
            OnSelectEvent?.Invoke();
        }
    }

    public void OnBack(InputAction.CallbackContext context)
    {
        if(context.performed)   
        {
            OnBackEvent?.Invoke();
        }
    }

    public void OnAnyKey(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnAnyKeyEvent?.Invoke();
        }
    }
}
