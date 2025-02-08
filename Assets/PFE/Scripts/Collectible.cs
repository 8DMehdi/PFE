

using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerRespawn>(); // Vérifie si c'est un joueur
        if (player != null)
        {
            CollectibleManager.Instance.AddCollectible(5); // Ajoute 5 points
            Destroy(gameObject); // Supprime l'objet collecté
        }
        SoundManager.Instance.PlayCollectSound();
    }
}
