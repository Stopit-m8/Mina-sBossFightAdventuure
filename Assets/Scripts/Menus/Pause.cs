using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private CanvasGroup pausePanel;
    [SerializeField] private PlayerInput playerInput;
    private bool isPaused = false;

    private void OnEnable()
    {
        playerInput.OnPauseInput += OpenClosePause;
    }

    private void OnDisable()
    {
        playerInput.OnPauseInput -= OpenClosePause;
    }

    private void RevealPanel(CanvasGroup panel)
    {
        panel.alpha = 1.0f;
        panel.interactable = true;
        panel.blocksRaycasts = true;
    }

    private void UnRevealPanel(CanvasGroup panel)
    {
        panel.alpha = 0f;
        panel.interactable = false;
        panel.blocksRaycasts = false;
    }

    public void OpenClosePause()
    {
        Debug.Log("Paused");
        if (!isPaused)
        {
            Time.timeScale = 0f;
            RevealPanel(pausePanel);
            isPaused = true;
        }
        else
        {
            Time.timeScale = 1f;
            UnRevealPanel(pausePanel);
            isPaused = false;
        }
    }
}
