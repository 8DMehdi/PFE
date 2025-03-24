using UnityEngine;
using UnityEngine.SceneManagement; // Nécessaire pour charger des scènes

public class SceneChanger : MonoBehaviour
{
    // Start est appelé au début de la scène
    void Start()
    {
        // Appel de la fonction pour changer de scène après 15 secondes
        Invoke("GoToScene1", 15f); // 15f représente 15 secondes
    }

    // Fonction qui charge la scène 1
    void GoToScene1()
    {
        // Remplacez "Scene1" par le nom exact de votre scène
        SceneManager.LoadScene(1);
    }
}