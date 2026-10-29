using System;
using UnityEngine;

public enum Sounds
{
    BGM,
    PlayerAttack,
    PlayerFootstep,
    BossStomp,
    BossHover,
    Missile
}

[ExecuteInEditMode]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("---Audio Source---")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("---Audio Clips---")]
    [SerializeField] private SoundList[] soundList;

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

    public static void PlaySound(Sounds sound)
    {
        AudioClip clips = Instance.soundList[(int)sound].Sounds;
        Instance.SFXSource.PlayOneShot(clips);
    }

    private void Start()
    {
        //musicSource.clip = musicClip;
        musicSource.Play();
    }
//#if UnityEditor
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(Sounds));
        Array.Resize(ref soundList, names.Length);
        for (int i = 0; i < soundList.Length; i++)
        {
            soundList[i].name = names[i];
        }
    }
//#endif
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    [Serializable]
    public struct SoundList
    {
        public AudioClip Sounds { get => sound;}
        [HideInInspector] public string name;
        [SerializeField] private AudioClip sound;
    }

}
