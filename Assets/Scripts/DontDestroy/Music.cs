using UnityEngine;
using System.Collections.Generic;

public class Music : MonoBehaviour
{
    static public Music instance;

    public List<AudioClip> audioClipList;
    public AudioClip click;

    private AudioSource[] audioSource;
    private AudioSource SoundEffectSource;
    private AudioSource SoundMusicSource;
    private Json.PlayerData player;

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

    void Start()
    {
        audioSource = gameObject.GetComponents<AudioSource>();
        SoundMusicSource = audioSource[0];
        SoundEffectSource = audioSource[1];

        player = Json.instance.Load();
        SoundEffectSource.volume = player.SoundEffect;
        SoundMusicSource.volume = player.SoundMusic;
    }

    void Update()
    {
        if (audioSource[0].isPlaying) return;
        PlayRandomMusic();
    }

    public void UpdateSoundMusic(float volume)
    {
        SoundMusicSource.volume = volume;
    }

    public void UpdateSoundEffect(float volume)
    {
        SoundEffectSource.volume = volume;
    }

    public void PlayMusic(AudioClip music)
    {
        SoundMusicSource.loop = true;
        SoundMusicSource.clip = music;
        SoundMusicSource.Play();
    }

    public void ClickSound()
    {
        SoundEffectSource.PlayOneShot(click);
    }

    public void SoundEffect(AudioClip sound)
    {
        SoundEffectSource.PlayOneShot(sound);
    }

    private void PlayRandomMusic()
    {
        SoundMusicSource.clip = GetRandom(audioClipList);
        SoundMusicSource.Play();
    }

    internal static T GetRandom<T>(IList<T> Params)
    {
        return Params[Random.Range(0, Params.Count)];
    }
}
