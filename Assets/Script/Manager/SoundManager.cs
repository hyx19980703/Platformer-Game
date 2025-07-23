using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource sfxSource;
    public AudioSource musicSource;

    public AudioClip[] soundEffects;

    public AudioClip bgm;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
            
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
                // 初始化音效源
        if (sfxSource == null)
            sfxSource = gameObject.AddComponent<AudioSource>();
        
        if (musicSource == null)
            musicSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySound(string soundName)
    {
        AudioClip clip = System.Array.Find(soundEffects, sound => sound.name == soundName); //寻找对应名称的soundEffects
        if (clip != null)
            sfxSource.PlayOneShot(clip); // 播放声音
    }

    public void PlayMusic()
    {
        musicSource.clip = bgm;
        musicSource.Play();
    }
}