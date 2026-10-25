using System;
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public event Action OnAttackFinished;
    public event Action<int> OnAttack;

    private bool _isAttacking;
    private bool _attackWindowOpen;
    private int _attackIndex;
    private Coroutine _comboWindowCoroutine;

    [SerializeField] private int maxAttackCombo = 3;
    [SerializeField] private float comboWindow = 0.3f;
    [SerializeField] private float attackTime = 0.4f;
    [SerializeField] private Collider2D attackArea;

    private void Awake()
    {
        attackArea.enabled = false;
    }

    private void OnEnable()
    {
        PlayerControl.Instance.PlayerInput.OnAttackInput += Attack;
    }

    private void OnDisable()
    {
        if (PlayerControl.Instance != null)
            PlayerControl.Instance.PlayerInput.OnAttackInput -= Attack;

        attackArea.enabled = false;
    }

    private void Attack()
    {
        if (!_isAttacking && _attackIndex == 0)
        {
            _attackIndex = 1;
            StartCoroutine(LaunchAttackEnumerator());
        }
        else if (_attackWindowOpen && _attackIndex < maxAttackCombo)
        {
            _attackWindowOpen = false;

            if (_comboWindowCoroutine != null)
            {
                StopCoroutine(_comboWindowCoroutine);
                _comboWindowCoroutine = null;
            }

            _attackIndex++;
            StartCoroutine(LaunchAttackEnumerator());
        }
        Debug.Log(_attackIndex);
    }

    private IEnumerator LaunchAttackEnumerator()
    {
        _isAttacking = true;

        OnAttack?.Invoke(_attackIndex);
        attackArea.enabled = true;
        yield return new WaitForSeconds(attackTime);
        attackArea.enabled = false;

        _isAttacking = false;
        OnAttackFinished?.Invoke();

        if (_attackIndex < maxAttackCombo)
        {
            _comboWindowCoroutine = StartCoroutine(StartComboWindow());
        }
        else
        {
            ResetAttack();
        }
    }

    private IEnumerator StartComboWindow()
    {
        _attackWindowOpen = true;

        yield return new WaitForSeconds(comboWindow);

        _attackWindowOpen = false;
        _comboWindowCoroutine = null;
        ResetAttack();
    }

    private void ResetAttack()
    {
        _attackIndex = 0;
        _isAttacking = false;
        _attackWindowOpen = false;
        _comboWindowCoroutine = null;
    }
}