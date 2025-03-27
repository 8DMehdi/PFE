using UnityEngine;
using UnityEngine.SceneManagement;

public class THEEND : MonoBehaviour
{
    // Nom de la scène que tu veux charger
    public string sceneToLoad;

    // Cette fonction est appelée lorsque l'objet entre dans le trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Vérifie si l'objet qui entre dans le trigger a le tag "Player"
        if (other.CompareTag("Player"))
        {
            // Charge la scène spécifiée
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}