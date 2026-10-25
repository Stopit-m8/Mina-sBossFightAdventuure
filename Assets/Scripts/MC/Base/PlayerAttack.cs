using System;
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public event Action OnAttackFinished;
    public event Action<int> OnAttack;

    private bool _isAttacking;
    private bool _attackWindowOpen;
    private int _attackIndex = 0;
    private float _rotation = 0f;
    private Coroutine _comboWindowCoroutine;

    [SerializeField] private float rotationOffset;
    [SerializeField] private int maxAttackCombo = 3;
    [SerializeField] private float comboWindow = 0.3f;
    [SerializeField] private float attackTime = 0.5f;
    [SerializeField] private Collider2D attackArea;

    private void Awake()
    {
        attackArea.enabled = false;
    }

    private void OnEnable()
    {
        PlayerControl.Instance.PlayerInput.OnAttackInput += Attack;
        PlayerControl.Instance.PlayerMovement.OnDirectionChanged += UpdateDirection;
    }

    private void OnDisable()
    {
        PlayerControl.Instance.PlayerInput.OnAttackInput -= Attack;
        PlayerControl.Instance.PlayerMovement.OnDirectionChanged -= UpdateDirection;
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
        transform.rotation = Quaternion.Euler(0f, 0f, _rotation);
        OnAttack?.Invoke(_attackIndex);
        attackArea.enabled = true;
        yield return new WaitForSeconds(attackTime);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
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

    private void UpdateDirection(Vector2 direction)
    {
        if (direction == Vector2.zero)
            return;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _rotation = angle + rotationOffset;
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