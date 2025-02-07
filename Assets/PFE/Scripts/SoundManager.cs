using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    // Références aux clips audio
    public AudioClip musicClip;
    public AudioClip jumpSound;
    public AudioClip collectSound;
    public AudioClip respawnSound;  // Son de respawn
    public AudioClip flySound;      // Son de vol
    public AudioClip landSound;     // Son d'atterrissage
    public AudioClip deathSound;    // Son de mort

    private AudioSource musicSource;
    private AudioSource effectsSource;

    public float musicVolume = 1f;
    public float effectsVolume = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }

        // Initialisation des AudioSources
        musicSource = gameObject.AddComponent<AudioSource>();
        effectsSource = gameObject.AddComponent<AudioSource>();

        // Configurer la musique pour qu'elle boucle
        musicSource.loop = true;
        musicSource.volume = musicVolume;
    }

    private void Start()
    {
        PlayMusic(musicClip); 
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    // Méthode pour jouer des effets sonores
    public void PlayEffect(AudioClip clip)
    {
        effectsSource.PlayOneShot(clip, effectsVolume);
    }

    // Méthode pour ajuster le volume de la musique
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        musicSource.volume = volume;
    }

    // Méthode pour ajuster le volume des effets sonores
    public void SetEffectsVolume(float volume)
    {
        effectsVolume = volume;
    }

    // Méthode pour couper la musique
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // Méthodes spécifiques pour chaque son
    public void PlayRespawnSound()
    {
        PlayEffect(respawnSound);
    }

public void PlayCollectSound()
    {
        PlayEffect(collectSound);
    }
    public void PlayFlySound()
    {
        PlayEffect(flySound);
    }

    public void PlayLandSound()
    {
        PlayEffect(landSound);
    }

    public void PlayDeathSound()
    {
        PlayEffect(deathSound);
    }
}
