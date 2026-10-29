using System;
using UnityEngine;

public class BossHealth : EntityHealth
{
    [SerializeField] private BossHealthUI bossHealthUI;

    public event Action<float, float> OnHealthChanged;

    private void Awake()
    {
        bossHealthUI.SetMaxHealth(MaxHealth);
        Debug.Log(MaxHealth);
    }

    public override void Damaged(float damageAmount)
    {
        base.Damaged(damageAmount);
        Debug.Log($"Boss health is = {CurrentHealth}");
        OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }
}
