// using UnityEngine;

// public class Checkpoint : MonoBehaviour
// {
//     private void OnTriggerEnter2D(Collider2D other)
//     {
//         CheckpointSystem playerCheckpoint = other.GetComponent<CheckpointSystem>();
        
//         if (playerCheckpoint != null)
//         {
//             playerCheckpoint.SetCheckpoint(transform.position); 
//         }
//     }
// }



using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        CheckpointSystem playerCheckpoint = other.GetComponent<CheckpointSystem>();

        if (playerCheckpoint != null)
        {
            playerCheckpoint.SetCheckpoint(transform.position); 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>(); // Vérifie si c'est le joueur
        if (player != null)
        {
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // Empêcher la perte de hauteur en maintenant la vélocité verticale positive ou nulle
                playerRb.velocity = new Vector2(playerRb.velocity.x, Mathf.Max(playerRb.velocity.y, 0));
            }
        }
    }
}
