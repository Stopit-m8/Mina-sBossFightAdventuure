using UnityEngine;

public class Win : MonoBehaviour
{
    private CanvasGroup _panel;
    [SerializeField] private BossHealth bossHealth;

    private void Awake()
    {
        _panel = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        bossHealth.OnDeath += RevealPanel;
    }

    private void OnDisable()
    {
        bossHealth.OnDeath -= RevealPanel;
    }

    private void RevealPanel()
    {
        Time.timeScale = 0f;
        _panel.alpha = 1.0f;
        _panel.interactable = true;
        _panel.blocksRaycasts = true;
    }
}
