using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private CinemachineCamera playerCamera;
    [SerializeField] private CinemachineCamera bossCamera;

    [Header("script")]
    [SerializeField] private BossAttack bossAttack;
    [SerializeField] private BossAnimation bossAnimation;

    private void OnEnable()
    {
        bossAttack.OnAttack += DetectIfSpecial; 
    }

    private void OnDisable()
    {
        bossAttack.OnAttack -= DetectIfSpecial;
    }

    private void DetectIfSpecial(bool isAttacking, bool isSpecialAttacking)
    {
        if (isSpecialAttacking)
        {
            ChangeToBossCamera();
        }
        else
        {
            ChangeToPlayerCamera();
        }
    }

    private void ChangeToBossCamera()
    {
        if (bossCamera.Priority == 10)
        {
            return;
        }
        bossCamera.Priority = 10;
        playerCamera.Priority = 0;
    }

    private void ChangeToPlayerCamera()
    {
        if (playerCamera.Priority == 10)
        {
            return;
        }
        playerCamera.Priority = 10;
        bossCamera.Priority = 0;
    }
}
