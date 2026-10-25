using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Rigidbody2D _rb;
    private Vector2 _direction = new();
    [SerializeField] private float speed;

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
        _rb.linearVelocity = new Vector2(_direction.x * speed, _direction.y * speed);
        Debug.Log(_rb.linearVelocity);
    }
}
