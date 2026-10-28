using System;
using UnityEngine;

public class BossAttackTrigger : MonoBehaviour
{
    public event Action OnSomethingOnTrigger;
    private void OnTriggerStay2D(Collider2D collision)
    {
        OnSomethingOnTrigger?.Invoke();
    }
}
