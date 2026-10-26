using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private Collider2D _attackArea;
    private float _attackDamage;
    [SerializeField] private ParticleSystem hitParticle;

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

            Vector2 closestPoint = collision.ClosestPoint(_attackArea.bounds.center);
            Instantiate(hitParticle, closestPoint, Quaternion.identity);

            enemy.Damaged(_attackDamage);
            Debug.Log($"{enemy} is damaged");
        }
    }
}
