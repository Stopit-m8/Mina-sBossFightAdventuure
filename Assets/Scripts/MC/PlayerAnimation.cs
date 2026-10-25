using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovement _movement;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponentInParent<PlayerMovement>();
    }

    private void OnEnable()
    {
        _movement.OnMovement += UpdateMovementAnimator;
    }

    private void UpdateMovementAnimator(Vector2 velocity, bool isMoving)
    {
        _animator.SetFloat("DirectionX", velocity.x);
        _animator.SetFloat("DirectionY", velocity.y);
        _animator.SetBool("IsMoving", isMoving);

    }
}
