

// using UnityEngine;

// public class Collectible : MonoBehaviour
// {
//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         var player = other.GetComponent<PlayerRespawn>(); // Vérifie si c'est un joueur
//         if (player != null)
//         {
//             CollectibleManager.Instance.AddCollectible(5); // Ajoute 5 points
//             Destroy(gameObject); // Supprime l'objet collecté
//         }
//         SoundManager.Instance.PlayCollectSound();
//     }
// }


using UnityEngine;

public class Collectible : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D other)
{

    var player = other.GetComponent<PlayerFlight>();
    if (player == null) player = other.GetComponentInParent<PlayerFlight>();

    {
        Debug.Log("Collectible ramassé ! Activation du vol...");
        player.UnlockFlight(); // Appelle la méthode UnlockFlight pour activer l'aptitude
        Destroy(gameObject);
        SoundManager.Instance.PlayCollectSound();
    }
}

}