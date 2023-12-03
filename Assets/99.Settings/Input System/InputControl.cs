using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "SO/Input Control")]
public class InputControl : ScriptableObject, Controls.IPlayerActions
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

    private Controls _inputControls;
    
    private void OnEnable()
    {
        if (_inputControls == null)
        {
            _inputControls = new Controls();
            _inputControls.Player.SetCallbacks(this);
        }
        
        _inputControls.Player.Enable();
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        MouseAimPosition = context.ReadValue<Vector2>();
    }
}
