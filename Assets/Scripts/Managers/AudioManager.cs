using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Header("Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clip BGM")]
    public AudioClip BGM;

    [Header("Audio Clip SFX")]
    public AudioClip PlayerAttack;
    public AudioClip BossAttack;
    public AudioClip Missile;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        musicSource.clip = BGM;
        musicSource.Play();
    }

    public void StopSFX()
    {
        sfxSource.Stop();
    }

    public void PlaySFX(AudioClip sfx)
    {
        sfxSource.PlayOneShot(sfx);
    }
}
