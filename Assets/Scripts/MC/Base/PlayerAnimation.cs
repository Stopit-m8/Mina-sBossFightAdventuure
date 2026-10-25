using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayerControl.Instance.PlayerMovement.OnMovement += UpdateMovementAnimator;
        PlayerControl.Instance.PlayerAttack.OnAttack += UpdateAttackAnimation;
    }

    private void OnDisable()
    {
        PlayerControl.Instance.PlayerMovement.OnMovement -= UpdateMovementAnimator;
    }

    private void UpdateMovementAnimator(Vector2 velocity, bool isMoving)
    {
        _animator.SetBool("IsMoving", isMoving);
        if (isMoving)
        {
            _animator.SetFloat("DirectionX", velocity.x);
            _animator.SetFloat("DirectionY", velocity.y);
        }
    }

    public void UpdateAttackAnimation(int comboIndex)
    {
        _animator.SetInteger("ComboIndex", comboIndex);
        _animator.SetTrigger("Attack");
    }
}
