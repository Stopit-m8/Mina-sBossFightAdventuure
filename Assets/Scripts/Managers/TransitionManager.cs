using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    IEnumerator NextSceneTransition()
    {
        Time.timeScale = 1f;
        //AudioManager.Instance.PlaySFX(AudioManager.Instance.ButtonClip);
        animator.SetTrigger("In");
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        animator.SetTrigger("Out");
    }

    IEnumerator PrevSceneTransition()
    {
        Time.timeScale = 1f;
        animator.SetTrigger("In");
        //AudioManager.Instance.PlaySFX(AudioManager.Instance.ButtonClip);
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        animator.SetTrigger("Out");
    }

    public void StartNextScene()
    {
        StartCoroutine(NextSceneTransition());
    }
    public void StartPrevScene()
    {
        StartCoroutine(PrevSceneTransition());
    }
}
