using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _direction = new();
    private Vector2 _lastDirection = new();
    private Vector2 _previousVelocity;
    private bool _isMoving = false;
    private bool _isDashing = false;
    private bool _canMove = true;
    private bool _canDash = true;
    private PlayerInput _playerInput;
    [SerializeField] private float speed;

    [Header("Dash")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;

    public event Action<Vector2, bool> OnMovement;
    public event Action<Vector2> OnDirectionChanged;
    public event Action<bool> OnDash;

    private void Awake()
    {
        _playerInput = PlayerControl.Instance.PlayerInput;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _playerInput.OnMovementInput += Movement;
        _playerInput.OnAttackInput += StopMovement;
        _playerInput.OnDashInput += Dash;
        PlayerControl.Instance.PlayerAttack.OnAttackFinished += StartMovement;
    }

    private void OnDisable()
    {
        _playerInput.OnMovementInput -= Movement;
        _playerInput.OnAttackInput -= StopMovement;
        _playerInput.OnDashInput -= Dash;
        PlayerControl.Instance.PlayerAttack.OnAttackFinished -= StartMovement;
    }

    private void Movement(Vector2 direction)
    {
        _direction = direction.normalized;
        if (_direction != Vector2.zero)
        {
            _lastDirection = _direction;
        }
        
        OnDirectionChanged?.Invoke(_direction);
    }

    private void StopMovement()
    {
        _canMove = false;
    }

    private void StartMovement()
    {
        _canMove = true;
    }

    private void FixedUpdate()
    {
        if (_canMove)
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
        else if(_isDashing)
        {
            _rb.AddForce(_lastDirection * dashSpeed, ForceMode2D.Impulse);
        }
        else
        {
            _rb.linearVelocity = Vector2.zero;
        }

    }

    private void Dash()
    {
        if (_canDash && _canMove)
        {
            StartCoroutine(DashCoroutine());
        }
        
    }

    IEnumerator DashCoroutine()
    {
        Debug.Log("Dash");

        _isDashing = true;
        OnDash?.Invoke(_isDashing);
        _canMove = false;

        yield return new WaitForSeconds(dashDuration);
        StartCoroutine(StartDashCooldown());

        _isDashing = false;
        OnDash?.Invoke(_isDashing);
        _canMove = true;
        
    }

    IEnumerator StartDashCooldown()
    {
        _canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        _canDash = true;
    }
}
