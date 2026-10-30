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
    private Collider2D _attackArea;

    [SerializeField] private float attackDamage;
    [SerializeField] private float rotationOffset;
    [SerializeField] private int maxAttackCombo = 3;
    [SerializeField] private float comboWindow = 0.3f;
    [SerializeField] private GameObject attackAreaObject;
    [SerializeField] private PlayerAnimation playerAnimation;

    private void Awake()
    {
        attackAreaObject.GetComponent<AttackArea>().SetDamage(attackDamage);
        _attackArea = attackAreaObject.GetComponent<Collider2D>();
        _attackArea.enabled = false;
        playerAnimation.OnAttackFinished += PostAttack;
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
            PreAttack();
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
            PreAttack();
        }
        Debug.Log(_attackIndex);
    }

    private void PreAttack()
    {
        _isAttacking = true;
        transform.rotation = Quaternion.Euler(0f, 0f, _rotation);
        OnAttack?.Invoke(_attackIndex);
        _attackArea.enabled = true;
    }

    private void PostAttack()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        _attackArea.enabled = false;

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