using System;
using UnityEngine;

public class BossHealth : EntityHealth
{
    public event Action<float, float> OnHealthChanged;
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
