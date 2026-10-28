using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Vector2 Direction { get; private set; }
    private bool _canInput = true;

    public event Action<Vector2> OnMovementInput;
    public event Action OnAttackInput;
    public event Action OnDashInput;

    public void Movement(InputAction.CallbackContext ctx)
    {
        if (!_canInput)
        {
            return;
        }
        Direction = ctx.ReadValue<Vector2>();
        Debug.Log("Player Input Movement");
        OnMovementInput?.Invoke(Direction);
    }

    public void Attack(InputAction.CallbackContext ctx)
    {
        if (!_canInput)
        {
            return;
        }
        if (ctx.performed)
        {
            Debug.Log("Player Attacked");
            OnAttackInput?.Invoke();
        }
    }

    public void Dash(InputAction.CallbackContext ctx)
    {
        if (!_canInput)
        {
            return;
        }
        if (ctx.performed)
        {
            Debug.Log("Dash Inputted");
            OnDashInput?.Invoke();
        }
    }

    public void Pause(InputAction.CallbackContext ctx)
    {
        if (!_canInput)
        {
            return;
        }
        if (ctx.performed)
        {

        }
    }

    public void ChangeCanInput(bool canInput)
    {
        _canInput = canInput;
    }
}
