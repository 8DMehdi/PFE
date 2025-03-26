

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