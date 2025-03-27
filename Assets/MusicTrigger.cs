using System.Collections;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
<<<<<<< HEAD
    public AudioSource musicSource; // La musique ï¿½ jouer
    public float fadeDuration = 1.0f; // Durï¿½e du fade in/out
=======
    public AudioSource musicSource; // La musique à jouer
    public float fadeDuration = 1.0f; // Durée du fade in/out
>>>>>>> 90c7b9640281cbed5e82d0c527182912a63702c9

    private Coroutine fadeCoroutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
<<<<<<< HEAD
        if (other.CompareTag("Player")) // Vï¿½rifie si c'est le joueur
        {
            Debug.Log("Joueur entrï¿½ dans la zone, activation de la musique");
=======
        if (other.CompareTag("Player")) // Vérifie si c'est le joueur
        {
            Debug.Log("Joueur entré dans la zone, activation de la musique");
>>>>>>> 90c7b9640281cbed5e82d0c527182912a63702c9
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeMusic(1f)); // Fade in
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
<<<<<<< HEAD
            Debug.Log("Joueur sorti de la zone, dï¿½sactivation de la musique");
=======
            Debug.Log("Joueur sorti de la zone, désactivation de la musique");
>>>>>>> 90c7b9640281cbed5e82d0c527182912a63702c9
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