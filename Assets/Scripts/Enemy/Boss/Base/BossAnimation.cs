using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private BossAttack bossAttack;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        bossAttack.OnAttack += AttackAnimation;
    }

    private void OnDisable()
    {
        bossAttack.OnAttack -= AttackAnimation;
    }

    private void AttackAnimation(bool isNotAttacking)
    {
        _animator.SetBool("IsAttacking", !isNotAttacking);
    }
}
