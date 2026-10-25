using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private Collider2D attackArea;

    private void Awake()
    {
        attackArea = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Something Got Attacked");
    }
}
