using System;
using System.Collections;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float attackDamage;
    [SerializeField] private GameObject bossAttackArea;
    [SerializeField] private Collider2D bossTriggerArea;
    [SerializeField] private BossAttackTrigger bossAttackTrigger;
    [SerializeField] private BossAnimation bossAnimation;

    [Header("Special Attack")]
    [SerializeField] private float specialAttackCooldown; 

    [Header("Normal Attack")]
    [SerializeField] private float normalAttackCooldown;

    
    
    
    private Collider2D _attackArea;
    private BossHealth _bossHealth;
    private bool _isAttacking = false;
    private bool _isSpecialAttacking = false;
    private bool _canSpecial = false;
    private bool _canAttack = true;
    private bool _specialUnlocked = false;

    public event Action<bool, bool> OnAttack;

    private void OnEnable()
    {
        _bossHealth.OnHealthChanged += AttackTypeChange;
        bossAttackTrigger.OnSomethingOnTrigger += SomethingOnTrigger;
        bossAnimation.OnAttackFinished += PostAttack;
    }

    private void OnDisable()
    {
        _bossHealth.OnHealthChanged -= AttackTypeChange;
        bossAttackTrigger.OnSomethingOnTrigger -= SomethingOnTrigger;
        bossAnimation.OnAttackFinished -= PostAttack;
    }

    private void Awake()
    {
        _bossHealth = GetComponent<BossHealth>();
        bossAttackArea.GetComponent<BossAttackArea>().SetDamage(attackDamage);
        _attackArea = bossAttackArea.GetComponent<Collider2D>();
        _attackArea.enabled = false;
    }

    private void Update()
    {
        Debug.Log($"can Attack = {_canAttack}");
        Debug.Log($"can Special = {_canSpecial}");
    }

    private void SomethingOnTrigger()
    {
        if (!_isAttacking && (_canAttack || (_specialUnlocked && _canSpecial)))
        {
            Debug.Log("player is in");
            Attack();
        }
    }

    private void Attack()
    {
        PreAttack();
        
    }

    private void PreAttack()
    {
        _canAttack = false;
        _isAttacking = true;
        
        _attackArea.enabled = true;
        if (_specialUnlocked && _canSpecial)
        {
            _attackArea.enabled = false;
            Debug.Log($"special is unlocked and ready");
            _isSpecialAttacking = true;
        }
        OnAttack?.Invoke(_isAttacking, _isSpecialAttacking);
    }

    private void PostAttack()
    {
        Debug.Log("Post Attack");
        _attackArea.enabled = false;
        _isAttacking = false;
        
        
        
        if (_isSpecialAttacking)
        {
            StartCoroutine(SpecialAttackCoroutine());
            _isSpecialAttacking = false;
        }
        else
        {
            StartCoroutine(NormalAttackCoroutine());
        }

        OnAttack?.Invoke(_isAttacking, _isSpecialAttacking);
    }

    IEnumerator NormalAttackCoroutine()
    {
        _canAttack = false;
        yield return new WaitForSeconds(normalAttackCooldown);
        _canAttack = true;
    }

    IEnumerator SpecialAttackCoroutine()
    {
        _canSpecial = false;
        yield return new WaitForSeconds(specialAttackCooldown);
        _canSpecial = true;
        _canAttack = true;
    }

    private void AttackTypeChange(float currentHealth, float maxHealth)
    {
        Debug.Log("AttackTpe Invoke");
        if (currentHealth < maxHealth/2)
        {
            _specialUnlocked = true;
            _canSpecial = true;
            Debug.Log($"special is unlocked {_specialUnlocked}");
        }
    }
}
