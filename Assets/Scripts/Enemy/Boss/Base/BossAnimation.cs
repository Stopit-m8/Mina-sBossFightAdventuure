using System;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private BossAttack bossAttack;

    public event Action OnAttackFinished;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        bossAttack.OnAttack += AttackAnimation;
        bossAttack.OnSpecialTriggered += SpecialAttackAnimation;
    }

    private void OnDisable()
    {
        bossAttack.OnAttack -= AttackAnimation;
        bossAttack.OnSpecialTriggered -= SpecialAttackAnimation;
    }

    private void AttackAnimation(bool isNotAttacking)
    {
        _animator.SetBool("IsAttacking", !isNotAttacking);
    }

    private void SpecialAttackAnimation(bool isSpecialAttacking)
    {
        _animator.SetBool("IsSpecial", isSpecialAttacking);
    }

    private void UpdateAttackAnimationStatus()
    {
        OnAttackFinished?.Invoke();
    }
}
