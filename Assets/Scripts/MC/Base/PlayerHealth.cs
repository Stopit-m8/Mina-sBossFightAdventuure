using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : EntityHealth
{
    private PlayerMovement _playerMovement;
    private bool _isImmune = false;
    [SerializeField] private float immuneTime;
    [SerializeField] private PlayerHealthUI playerHealthUI;
    public event Action<float> OnHealthChanged;

    private void Awake()
    {
        _playerMovement = PlayerControl.Instance.PlayerMovement;
        playerHealthUI.SetMaxHealth(MaxHealth);
    }

    private void OnEnable()
    {
        _playerMovement.OnDash += Immune;
    }

    private void OnDisable()
    {
        _playerMovement.OnDash -= Immune;
    }

    public override void Damaged(float damageAmount)
    {
        if (!_isImmune)
        {
            
            base.Damaged(damageAmount);
            Debug.Log($"Player Health = {CurrentHealth}");
            OnHealthChanged?.Invoke(CurrentHealth);
            StartCoroutine(AfterHit());
        }
        else
        {
            Debug.Log("Is Immune");
        }
        
    }

    private void Immune(bool isImmune)
    {
        _isImmune = isImmune;
        Debug.Log($"Immune = {_isImmune}");
    }

    IEnumerator AfterHit()
    {
        _isImmune = true;
        yield return new WaitForSeconds(immuneTime);
        _isImmune = false;
    }

    public override void Die()
    {
        base.Die();
        Debug.Log("Player ded");
    }
}
