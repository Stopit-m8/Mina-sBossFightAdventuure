using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Vector2 Direction { get; private set; }

    public event Action<Vector2> OnMovementInput;
    public event Action OnAttackInput;

    public void Movement(InputAction.CallbackContext ctx)
    {
        Direction = ctx.ReadValue<Vector2>();
        Debug.Log("Player Input Movement");
        OnMovementInput?.Invoke(Direction);
    }

    public void Attack(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            OnAttackInput?.Invoke();
        }
    }

    public void Pause(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {

        }
    }
}
