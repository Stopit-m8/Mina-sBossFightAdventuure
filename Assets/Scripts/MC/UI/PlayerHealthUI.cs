using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealthUI : MonoBehaviour
{
    private Tween _healthTween;
    private Tween _backHealthTween;

    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider healthBarBack;
    [SerializeField] private float healthDecreaseDuration;
    [SerializeField] private float backDelayDuration;
    private PlayerHealth _playerHealth;

    private void Awake()
    {
        _playerHealth = PlayerControl.Instance.PlayerHealth;
    }

    private void OnEnable()
    {
        _playerHealth.OnHealthChanged += SetHealth;
    }

    private void OnDisable()
    {
        _playerHealth.OnHealthChanged -= SetHealth;
    }

    public void SetMaxHealth(float maxHealth)
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
            
        healthBarBack.maxValue = maxHealth;
        healthBarBack.value = maxHealth;
    }

    private void SetHealth(float currentHealth)
    {
        _healthTween?.Kill();
        _backHealthTween?.Kill();

        _healthTween = healthBar.DOValue(currentHealth, healthDecreaseDuration).SetEase(Ease.OutQuad);
        _backHealthTween = DOTween.Sequence().AppendInterval(backDelayDuration).Append(healthBarBack.DOValue(currentHealth, healthDecreaseDuration).SetEase(Ease.OutQuad));
    }
}
