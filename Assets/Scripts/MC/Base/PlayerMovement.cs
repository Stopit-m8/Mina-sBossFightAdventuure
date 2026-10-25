using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _direction = new();
    private Vector2 _previousVelocity;
    private bool _isMoving = false;
    public bool canMove = true;
    [SerializeField] private float speed;

    public event Action<Vector2, bool> OnMovement;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        PlayerControl.Instance.PlayerInput.OnMovementInput += Movement;
        PlayerControl.Instance.PlayerInput.OnAttackInput += StopMovement;
        PlayerControl.Instance.PlayerAttack.OnAttackFinished += StartMovement;
    }

    private void OnDisable()
    {
        PlayerControl.Instance.PlayerInput.OnMovementInput -= Movement;
        PlayerControl.Instance.PlayerInput.OnAttackInput -= StopMovement;
        PlayerControl.Instance.PlayerAttack.OnAttackFinished -= StartMovement;
    }

    private void Movement(Vector2 direction)
    {
        _direction = direction.normalized;
        Debug.Log("Direction Inputted");
    }

    private void StopMovement()
    {
        canMove = false;
    }

    private void StartMovement()
    {
        canMove = true;
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            _rb.linearVelocity = new Vector2(_direction.x * speed, _direction.y * speed);
            if (_rb.linearVelocity != _previousVelocity)
            {
                _previousVelocity = _rb.linearVelocity;
                if (_previousVelocity == Vector2.zero)
                {
                    _isMoving = false;
                }
                else
                {
                    _isMoving = true;
                }
                OnMovement?.Invoke(_previousVelocity, _isMoving);
            }
        }
        else
        {
            _rb.linearVelocity = Vector2.zero;
        }

            Debug.Log(_rb.linearVelocity);
    }
}
