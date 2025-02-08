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
}
