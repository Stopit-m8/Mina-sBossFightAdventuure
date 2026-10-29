using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    private Tween _healthTween;
    private Tween _backHealthTween;

    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider healthBarBack;
    [SerializeField] private float healthDecreaseDuration;
    [SerializeField] private float backDelayDuration;

    [SerializeField] private BossHealth bossHealth;

    private void OnEnable()
    {
        bossHealth.OnHealthChanged += SetHealth;
    }

    public void SetMaxHealth(float maxHealth)
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;

        healthBarBack.maxValue = maxHealth;
        healthBarBack.value = maxHealth;
    }

    private void SetHealth(float currentHealth, float maxHealth)
    {
        _healthTween?.Kill();
        _backHealthTween?.Kill();

        _healthTween = healthBar.DOValue(currentHealth, healthDecreaseDuration).SetEase(Ease.OutQuad);
        _backHealthTween = DOTween.Sequence().AppendInterval(backDelayDuration).Append(healthBarBack.DOValue(currentHealth, healthDecreaseDuration).SetEase(Ease.OutQuad));
    }
}
