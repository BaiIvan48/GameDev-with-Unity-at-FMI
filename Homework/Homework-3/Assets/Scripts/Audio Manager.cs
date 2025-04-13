using UnityEngine;

//////// Music and SFX in this game are thanks to https://www.youtube.com/watch?v=N8whM1GjH4w&list=WL&index=6
public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Source ----------")]
    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource SFXSource;

    [Header("---------- Audio Clip ----------")]
    public AudioClip background;
    public AudioClip jump;
    public AudioClip hit;
    public AudioClip key;
    public AudioClip respawn;
    public AudioClip win;
    public AudioClip lose;
    public AudioClip drop_platform;
    public AudioClip trampoline;
    public AudioClip lightning_pickup;


    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void StopMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    public void PlayMusic()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

    public void SetMusicPitch(float pitch)
    {
        musicSource.pitch = pitch;
    }
}
