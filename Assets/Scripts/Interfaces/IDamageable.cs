using UnityEngine;

public interface IDamageable
{
    void Damaged(float damageAmount);
    void Die();
    float MaxHealth { get; set; }
    float CurrentHealth {  get; set; }
}
