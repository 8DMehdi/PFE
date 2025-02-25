using UnityEngine;

public class GrappleCollectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null) // Vérifie si l'objet qui entre en collision est bien le joueur
        {
            player.UnlockGrappling();
            Destroy(gameObject); // Supprime le collectible après l'avoir ramassé
        }
    }
}
