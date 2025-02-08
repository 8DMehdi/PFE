using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private CheckpointSystem checkpointSystem;

    private void Start()
    {
        checkpointSystem = GetComponent<CheckpointSystem>();
    }

    public void Respawn()
    {
        if (checkpointSystem != null)
        {
            transform.position = checkpointSystem.GetCheckpoint(); // Se téléporte au dernier checkpoint
        }
        else
        {
            Debug.LogWarning("CheckpointSystem non trouvé sur le joueur !");
        }
        SoundManager.Instance.PlayRespawnSound();
    }
}
