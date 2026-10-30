using System.Collections;
using UnityEngine;

public class BossAttackArea : MonoBehaviour
{
    private Collider2D _attackArea;
    private float _attackDamage;
    [SerializeField] private float freezeTimeDuration;

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
        Debug.Log($"Something Got Attacked = {collision.name}");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Got Player");
            collision.TryGetComponent<IDamageable>(out IDamageable player);
            FreezeOnHit();

            player.Damaged(_attackDamage);
            Debug.Log($"{player} is damaged");
        }
    }

    private void FreezeOnHit()
    {
        StartCoroutine(Freeze());
    }

    IEnumerator Freeze()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(freezeTimeDuration);
        Time.timeScale = 1f;

    }
}
