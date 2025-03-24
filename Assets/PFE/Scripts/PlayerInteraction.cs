using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 2f;
    private bool isPlayerNearby = false;
    private Interactable currentNPC;
    private PlayerController playerController; // Référence au PlayerController
//    private IsLastNPC  isLastNPC;
    private void Start()
    {
        // Récupère la référence du PlayerController au début
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
{
    if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
    {
        if (DialogueManager.Instance.dialoguePanel.activeSelf)
        {
            DialogueManager.Instance.DisplayNextSentence();
        }
        else
        {
            if (currentNPC != null)
            {
                DialogueManager.Instance.StartDialogue(currentNPC.dialogue);

                // Vérifie si c'est bien le dernier NPC avant de débloquer le vol
                NPC npc = currentNPC.GetComponent<NPC>();
                if (npc != null && npc.isLastNPC)
                {
                    UnlockFlyingAbility();
                }
            }
        }
    }
}


    private void UnlockFlyingAbility()
    {
        if (playerController != null)
        {
            playerController.EnableFly(); // Active la capacité de voler
            Debug.Log("Le vol a été débloqué après l'interaction avec le PNJ.");
        }
    }

private void OnTriggerEnter2D(Collider2D other)
{
    Interactable interactable = other.GetComponent<Interactable>();
    NPC npc = other.GetComponent<NPC>(); // Vérifie si c'est un NPC

    if (interactable != null)
    {
        isPlayerNearby = true;
        currentNPC = interactable;

        // Vérifie si c'est le dernier NPC
        if (npc != null && npc.isLastNPC) 
        {
            PlayerController playerController = FindObjectOfType<PlayerController>();
            playerController.hasTalkedToLastNPC = true;
            Debug.Log("Dernier NPC trouvé ! Le vol est maintenant déblocable.");
        }
    }
}


private void OnTriggerExit2D(Collider2D other)
{
    if (other.GetComponent<Interactable>() != null)
    {
        isPlayerNearby = false;
        currentNPC = null;
    }
}
}
