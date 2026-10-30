using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void PlayGame()
    {
        TransitionManager.Instance.StartNextScene();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ToMainMenu()
    {
        TransitionManager.Instance.StartPrevScene();
    }

    public void RestartScene()
    {
        TransitionManager.Instance.RestartScene();
    }
}
