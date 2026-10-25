using System;
using UnityEngine;

public class PlayerHealth : EntityHealth
{
    public event Action OnHealthChanged;
    public override void Damaged(float damageAmount)
    {
        base.Damaged(damageAmount);
        Debug.Log($"Player Health = {CurrentHealth}");

    }

    public override void Die()
    {
        base.Die();
        Debug.Log("Player ded");
    }
}
