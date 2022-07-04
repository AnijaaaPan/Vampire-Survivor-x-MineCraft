using UnityEngine;
using System.Collections.Generic;

public class Music : MonoBehaviour
{
    static public Music instance;

    public List<AudioClip> audioClipList;
    public AudioClip click;

    private Json.PlayerData player;
    private AudioSource audioSource;

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
        player = Json.instance.Load();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (player.SoundMusic != audioSource.volume)
        {
            audioSource.volume = player.SoundMusic;
        }
        if (audioSource.isPlaying) return;
        PlayRandomMusic();
    }

    public void UpdatePlayerJson()
    {
        player = Json.instance.Load();
    }

    private void PlayRandomMusic()
    {
        audioSource.clip = GetRandom(audioClipList);
        audioSource.Play();
    }

    public void PlayMusic(AudioClip music)
    {
        audioSource.loop = true;
        audioSource.clip = music;
        audioSource.Play();
    }

    public void ClickSound()
    {
        audioSource.PlayOneShot(click, player.SoundEffect);
    }

    internal static T GetRandom<T>(IList<T> Params)
    {
        return Params[Random.Range(0, Params.Count)];
    }
}
