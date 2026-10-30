using System;
using System.Collections;
using UnityEngine;

public class BossHealth : EntityHealth
{
    [SerializeField] private BossHealthUI bossHealthUI;
    [SerializeField] private ParticleSystem deathParticle;

    public event Action<float, float> OnHealthChanged;
    public event Action OnDeath;

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
        deathParticle.Play();
        StartCoroutine(Death());
        
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(1f);
        OnDeath?.Invoke();
    }
}
