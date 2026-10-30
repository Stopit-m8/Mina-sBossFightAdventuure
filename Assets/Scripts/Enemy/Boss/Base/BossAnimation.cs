using DG.Tweening;
using System;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private BossAttack bossAttack;
    [SerializeField] private BossHealth bossHealth;
    [SerializeField] private ParticleSystem stompParticle;

    public event Action OnAttackFinished;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        bossAttack.OnAttack += AttackAnimation;
        bossHealth.OnHealthChanged += OnHit;
    }

    private void OnDisable()
    {
        bossAttack.OnAttack -= AttackAnimation;
        bossHealth.OnHealthChanged -= OnHit;
    }

    private void AttackAnimation(bool isAttacking, bool isSpecialAttacking)
    {
        Debug.Log($"IsAttacking = {isAttacking}, IsSpecialAttacking = {isSpecialAttacking}");
        _animator.SetBool("IsAttacking", isAttacking);
        _animator.SetBool("IsSpecial", isSpecialAttacking);
    }

    private void OnHit(float currentHealth, float maxHealth)
    {
        Sequence _onHitSequence = DOTween.Sequence();
        _onHitSequence.Append(_spriteRenderer.DOColor(Color.red, 0.25f))
              .Join(transform.DOScale(1.1f, 0.25f))
              .Append(_spriteRenderer.DOColor(Color.white, 0.25f))
              .Join(transform.DOScale(1f, 0.25f));
    }

    private void StompAnimation()
    {
        stompParticle.Play();
    }

    private void UpdateAttackAnimationStatus()
    {
        OnAttackFinished?.Invoke();
    }
}
