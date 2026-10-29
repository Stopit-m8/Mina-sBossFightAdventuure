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
    }

    private void OnDisable()
    {
        bossAttack.OnAttack -= AttackAnimation;
    }

    private void AttackAnimation(bool isAttacking, bool isSpecialAttacking)
    {
        Debug.Log($"IsAttacking = {isAttacking}, IsSpecialAttacking = {isSpecialAttacking}");
        _animator.SetBool("IsAttacking", isAttacking);
        _animator.SetBool("IsSpecial", isSpecialAttacking);
    }

    private void UpdateAttackAnimationStatus()
    {
        OnAttackFinished?.Invoke();
    }
}
