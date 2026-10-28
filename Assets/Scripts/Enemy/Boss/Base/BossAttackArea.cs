using UnityEngine;

public class BossAttackArea : MonoBehaviour
{
    private Collider2D _attackArea;
    private float _attackDamage;

    private void Awake()
    {
        _attackArea = GetComponent<Collider2D>();
        _attackArea.enabled = false;
    }

    public void SetDamage(float damage)
    {
        _attackDamage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Something Got Attacked");
        if (collision.CompareTag("Player"))
        {
            collision.TryGetComponent<IDamageable>(out IDamageable player);

            player.Damaged(_attackDamage);
            Debug.Log($"{player} is damaged");
        }
    }
}
