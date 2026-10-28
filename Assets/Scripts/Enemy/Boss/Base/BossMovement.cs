using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float speed;
    private Vector2 _direction;
    private Rigidbody2D _rb;
    private bool _canMove = true;
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
        if (_canMove)
        {
            _direction = (player.transform.position - transform.position).normalized;
            _rb.linearVelocity = new Vector2(_direction.x * speed, _direction.y * speed);
        }
        else
        {
            Debug.Log("Boss Stop");
            _rb.linearVelocity = Vector2.zero;
        }
    }

    public void SetCanMove(bool canMove)
    {
        _canMove = canMove;
        Debug.Log($"can move is changed to {_canMove}");
    }
}
