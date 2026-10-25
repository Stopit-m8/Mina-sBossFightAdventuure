using UnityEngine;

public class EntityHealth : MonoBehaviour, IDamageable
{
    [field: SerializeField] public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("This Thing Dead");
    }
}
