using UnityEngine;

public class TestAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IDamageable player;
            collision.TryGetComponent<IDamageable>(out player);
            player.Damaged(10f);
        }
    }
}
