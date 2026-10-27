using Unity.Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private CinemachineImpulseSource impulseSource;

    public void ShakeCamera()
    {
        impulseSource.GenerateImpulse();
    }
}
