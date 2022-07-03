using UnityEngine;
using System.Collections.Generic;

public class Music : MonoBehaviour
{
    static public Music instance;

    public List<AudioClip> audioClipList;
    public AudioClip click;

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
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (audioSource.isPlaying) return;
        PlayRandomMusic();
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
        audioSource.PlayOneShot(click);
    }

    internal static T GetRandom<T>(IList<T> Params)
    {
        return Params[Random.Range(0, Params.Count)];
    }
}
