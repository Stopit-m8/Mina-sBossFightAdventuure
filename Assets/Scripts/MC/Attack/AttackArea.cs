using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private Collider2D _attackArea;
    private float _attackDamage;

    private void Awake()
    {
        _attackArea = GetComponent<Collider2D>();
    }

    public void SetDamage(float damage)
    {
        _attackDamage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Something Got Attacked");
        if (collision.CompareTag("Enemy"))
        {
            IDamageable enemy;
            collision.TryGetComponent<IDamageable>(out enemy);

            enemy.Damaged(_attackDamage);
            Debug.Log($"{enemy} is damaged");
        }
    }
}
