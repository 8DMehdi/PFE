using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 2f;
    private bool isPlayerNearby = false;
    private Interactable currentNPC; 

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
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null)
        {
            isPlayerNearby = true;
            currentNPC = interactable; 
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
