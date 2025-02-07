using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    private Vector2 lastCheckpointPosition;

    private void Start()
    {
        // Initialiser avec la position actuelle du joueur au cas où aucun checkpoint n'a encore été activé
        lastCheckpointPosition = transform.position;
    }

    public void SetCheckpoint(Vector2 newCheckpoint)
    {
        lastCheckpointPosition = newCheckpoint;
    }

    public Vector2 GetCheckpoint()
    {
        return lastCheckpointPosition;
    }
}
