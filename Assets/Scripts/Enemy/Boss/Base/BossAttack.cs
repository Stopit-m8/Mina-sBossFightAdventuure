using System;
using System.Collections;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] private float attackDamage;
    [SerializeField] private GameObject bossAttackArea;
    [SerializeField] private Collider2D bossTriggerArea;
    [SerializeField] private BossAttackTrigger bossAttackTrigger;
    [SerializeField] private float normalAttackTime;
    [SerializeField] private float specialAttackTime;
    [SerializeField] private float normalAttackCooldown;
    [SerializeField] private float specialAttackCooldown;
    private Collider2D _attackArea;
    private BossHealth _bossHealth;
    private bool _isAttacking = false;
    private enum BossAttackType
    {
        Special,
        Normal
    }

    private BossAttackType _attackType = BossAttackType.Normal;

    public event Action<bool> OnAttack;

    private void OnEnable()
    {
        _bossHealth.OnHealthChanged += AttackTypeChange;
        bossAttackTrigger.OnSomethingOnTrigger += SomethingOnTrigger;
    }

    private void OnDisable()
    {
        _bossHealth.OnHealthChanged -= AttackTypeChange;
        bossAttackTrigger.OnSomethingOnTrigger -= SomethingOnTrigger;
    }

    private void Awake()
    {
        _bossHealth = GetComponent<BossHealth>();
        bossAttackArea.GetComponent<BossAttackArea>().SetDamage(attackDamage);
        _attackArea = bossAttackArea.GetComponent<Collider2D>();
        _attackArea.enabled = false;
    }

    private void SomethingOnTrigger()
    {
        if (!_isAttacking)
        {
            Debug.Log("player is in");
            Attack();
        }
    }

    private void Attack()
    {
        
        switch (_attackType)
        {
            case BossAttackType.Special:
                SpecialAttack();
                break;

            case BossAttackType.Normal:
                NormalAttack();
                break;
        }
        
    }

    private void NormalAttack()
    {
        Debug.Log("boss normal attack");
        StartCoroutine(NormalAttackCoroutine());

    }

    private void SpecialAttack()
    {
        Debug.Log("boss Special attack");
        StartCoroutine(SpecialAttackCoroutine());
    }

    IEnumerator NormalAttackCoroutine()
    {
        
        _isAttacking = true;
        OnAttack?.Invoke(!_isAttacking);
        _attackArea.enabled = true;
        yield return new WaitForSeconds(normalAttackTime);
        _attackArea.enabled = false;
        yield return new WaitForSeconds(normalAttackCooldown);
        _isAttacking = false;
        OnAttack?.Invoke(!_isAttacking);
    }

    IEnumerator SpecialAttackCoroutine()
    {

        _isAttacking = true;
        OnAttack?.Invoke(!_isAttacking);

        yield return new WaitForSeconds(specialAttackTime);

        yield return new WaitForSeconds(specialAttackCooldown);
        _isAttacking = false;
        OnAttack?.Invoke(!_isAttacking);
    }

    private void AttackTypeChange(float currentHealth, float maxHealth)
    {
        if (currentHealth < maxHealth/2)
        {
            _attackType = BossAttackType.Special;
        }
        else
        {
            _attackType = BossAttackType.Normal;
        }
    }
}
