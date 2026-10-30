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
    private BossHealth _bossHealth;

    [Header("Special Attack")]
    [SerializeField] private float specialAttackCooldown;
    [SerializeField] private float specialAttackInterval;
    [SerializeField] private Transform spawnCenter;
    [SerializeField] private float spawnRadius = 5f;
    [SerializeField] private ProjectilePooling pool;
    [SerializeField] private ParticleSystem chargeParticle;
    private float _specialAttackTimer = 0;
    private bool _specialUnlocked = false;
    private bool _isSpecialAttacking = false;
    private bool _canSpecial = true;

    [Header("Normal Attack")]
    [SerializeField] private float normalAttackCooldown;
    private Collider2D _attackArea;
    private bool _isAttacking = false;
    private bool _canAttack = true;
    

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
        if (_isSpecialAttacking)
        {
            SpecialAttack();
        }
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

    private void SpecialAttack()
    {
        chargeParticle.Play();
        _specialAttackTimer += Time.deltaTime;
        if (_specialAttackTimer >= specialAttackInterval)
        {

            SpawnMissile();
            _specialAttackTimer = 0f;
        }
    }

    private void SpawnMissile()
    {
        Vector2 randomPosition = (Vector2)spawnCenter.position + UnityEngine.Random.insideUnitCircle * spawnRadius;
        GameObject obj = pool.GetPooledObject();

        if (obj == null) return;

        obj.transform.position = randomPosition;
        obj.SetActive(true);
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
            SpecialAttack();
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
            chargeParticle.Stop();
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
            Debug.Log($"special is unlocked {_specialUnlocked}");
        }
    }
}
