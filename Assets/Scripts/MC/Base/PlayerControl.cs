using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance;
    public PlayerAttack PlayerAttack { get; private set;}
    public PlayerMovement PlayerMovement { get; private set;}
    public PlayerInput PlayerInput { get; private set;}
    public PlayerHealth PlayerHealth { get; private set;}

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        PlayerAttack = GetComponent<PlayerAttack>();
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerHealth = GetComponent<PlayerHealth>();
        PlayerInput = GetComponent<PlayerInput>();
    }
}
