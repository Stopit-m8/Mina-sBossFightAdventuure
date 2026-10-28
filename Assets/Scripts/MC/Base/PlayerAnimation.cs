using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private ParticleSystem slashEffect;

    public event Action OnAttackFinished;

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
        PlayerControl.Instance.PlayerAttack.OnAttack -= UpdateAttackAnimation;
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

    private void UpdateAttackAnimation(int comboIndex)
    {
        _animator.SetInteger("ComboIndex", comboIndex);
        slashEffect.Play();
        _animator.SetTrigger("Attack");
    }

    private void UpdateAttackAnimationStatus()
    {
        OnAttackFinished?.Invoke();
    }
}
