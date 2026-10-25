using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance;
    public PlayerAttack PlayerAttack;
    public PlayerMovement PlayerMovement;
    public PlayerInput PlayerInput;
    public PlayerHealth PlayerHealth;

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
