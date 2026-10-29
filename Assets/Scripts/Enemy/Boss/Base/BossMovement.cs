using DG.Tweening;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    [SerializeField] private Transform specialAttackPlace;
    private Vector2 _direction;
    private Rigidbody2D _rb;
    private bool _canMove = true;
    private bool _isSpecialing = false;
    private BossAttack _bossAttack;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _bossAttack = GetComponent<BossAttack>();
    }

    private void OnEnable()
    {
        _bossAttack.OnAttack += SetCanMove;
    }

    private void OnDisable()
    {
        _bossAttack.OnAttack -= SetCanMove;
    }

    private void FixedUpdate()
    {
        if (_canMove && !_isSpecialing)
        {
            _direction = (player.transform.position - transform.position).normalized;
            _rb.linearVelocity = _direction * speed;
        }
        else
        {
            _rb.linearVelocity = Vector2.zero;
        }
    }

    private void SetCanMove(bool isAttacking, bool isSpecial)
    {
        _canMove = !isAttacking;
        _isSpecialing = isSpecial;
        Debug.Log($"isSpecial = {isSpecial}");

        if (_isSpecialing)
        {
            _canMove = false;
            _rb.linearVelocity = Vector2.zero;

            transform.DOKill();
            transform.DOMove(specialAttackPlace.position, 1f);
        }
    }

    public void RestrictBossMovement(bool restrict)
    {
        _canMove = !restrict;
    }
}
