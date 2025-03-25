using System.Collections;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public AudioSource musicSource; // La musique à jouer
    public float fadeDuration = 1.0f; // Durée du fade in/out

    private Coroutine fadeCoroutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Vérifie si c'est le joueur
        {
            Debug.Log("Joueur entré dans la zone, activation de la musique");
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeMusic(1f)); // Fade in
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Joueur sorti de la zone, désactivation de la musique");
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeMusic(0f)); // Fade out
        }
    }

    private IEnumerator FadeMusic(float targetVolume)
    {
        float startVolume = musicSource.volume;
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, targetVolume, time / fadeDuration);
            yield return null;
        }

        musicSource.volume = targetVolume;

        if (targetVolume == 0f)
            musicSource.Pause();
        else if (!musicSource.isPlaying)
            musicSource.Play();
    }
}