using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Rigidbody2D _rb;
    private Vector2 _direction = new();
    private Vector2 _previousVelocity;
    private bool _isMoving = false;
    public bool canMove = true;
    [SerializeField] private float speed;

    public event Action<Vector2, bool> OnMovement;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _playerInput.OnMovementInput += Movement;
    }

    private void OnDisable()
    {
        _playerInput.OnMovementInput -= Movement;
    }

    private void Movement(Vector2 direction)
    {
        _direction = direction.normalized;
        Debug.Log("Direction Inputted");
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
        
        Debug.Log(_rb.linearVelocity);
    }
}
