using UnityEngine;

public class BossHealth : EntityHealth
{
    public override void Damaged(float damageAmount)
    {
        base.Damaged(damageAmount);
        Debug.Log($"Boss health is = {CurrentHealth}");
    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }
}
