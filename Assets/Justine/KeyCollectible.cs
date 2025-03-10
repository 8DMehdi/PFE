// using UnityEngine;

// public class KeyCollectible : MonoBehaviour
// {
//     public Door door;  // Référence à la porte à ouvrir

//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         // Vérifie si l'objet entrant a le composant PlayerController
//         PlayerController player = other.GetComponent<PlayerController>();
//         if (player != null)
//         {
//             // Si c'est un joueur, appelle la méthode pour ouvrir la porte
//             door.UnlockDoor();  // Ouvre la porte
//             Destroy(gameObject); // Détruit la clé après l'avoir ramassée
//         }
//     }
// }



using UnityEngine;

public class KeyCollectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.hasKey = true; // Donne la clé au joueur
            Destroy(gameObject);  // Détruit la clé après l'avoir ramassée
        }
    }
}
