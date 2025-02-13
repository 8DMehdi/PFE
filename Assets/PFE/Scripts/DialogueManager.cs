using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject dialoguePanel; // Le panneau de dialogue
    public Text nameText; // Texte pour le nom du NPC
    public Text dialogueText; // Texte pour le dialogue
    public float typingSpeed = 0.05f; // Vitesse d'écriture du texte

    private Queue<string> npcSentences; // Phrases du NPC
    private Queue<string> playerSentences; // Phrases du joueur
    private bool isPlayerTurn = false; // Indique si c'est au tour du joueur

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        npcSentences = new Queue<string>();
        playerSentences = new Queue<string>();
        dialoguePanel.SetActive(false);
    }

    // public void StartDialogue(Dialogue dialogue)
    // {
    //     if (dialogue == null || dialogue.npcSentences.Count == 0)
    //     {
    //         Debug.LogError("Le dialogue est vide ou non assigné !");
    //         return;
    //     }

    //     npcSentences.Clear();
    //     playerSentences.Clear();

    //     nameText.text = dialogue.npcName;

    //     foreach (string sentence in dialogue.npcSentences)
    //     {
    //         npcSentences.Enqueue(sentence);
    //     }

    //     foreach (string sentence in dialogue.playerSentences)
    //     {
    //         playerSentences.Enqueue(sentence);
    //     }

    //     dialoguePanel.SetActive(true);
    //     isPlayerTurn = false;
    //     DisplayNextSentence();
    // }

public void StartDialogue(DialogueData dialogue)
{
    if (dialogue == null || dialogue.npcSentences.Count == 0)
    {
        Debug.LogError("Le dialogue est vide ou non assigné !");
        return;
    }

    npcSentences.Clear();
    playerSentences.Clear();

    nameText.text = dialogue.npcName;

    foreach (string sentence in dialogue.npcSentences)
    {
        npcSentences.Enqueue(sentence);
    }

    foreach (string sentence in dialogue.playerSentences)
    {
        playerSentences.Enqueue(sentence);
    }

    dialoguePanel.SetActive(true);
    isPlayerTurn = false;
    DisplayNextSentence();
}


public void DisplayNextSentence()
{
    if (!isPlayerTurn && npcSentences.Count > 0)
    {
        string sentence = npcSentences.Dequeue();
        Debug.Log("NPC dit : " + sentence);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        isPlayerTurn = true;
    }
    else if (isPlayerTurn && playerSentences.Count > 0)
    {
        string sentence = playerSentences.Dequeue();
        Debug.Log("Joueur dit : " + sentence);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        isPlayerTurn = false;
    }
    else
    {
        EndDialogue();
    }
}


    // public void DisplayNextSentence()
    // {
    //     if (!isPlayerTurn && npcSentences.Count > 0)
    //     {
    //         string sentence = npcSentences.Dequeue();
    //         StopAllCoroutines();
    //         StartCoroutine(TypeSentence(sentence));
    //         isPlayerTurn = true;
    //     }
    //     else if (isPlayerTurn && playerSentences.Count > 0)
    //     {
    //         string sentence = playerSentences.Dequeue();
    //         StopAllCoroutines();
    //         StartCoroutine(TypeSentence(sentence));
    //         isPlayerTurn = false;
    //     }
    //     else
    //     {
    //         EndDialogue();
    //     }
    // }

    // IEnumerator TypeSentence(string sentence)
    // {
    //     dialogueText.text = "";
    //     foreach (char letter in sentence.ToCharArray())
    //     {
    //         dialogueText.text += letter;
    //         yield return new WaitForSeconds(typingSpeed);
    //     }
    // }

private bool isTyping = false;

IEnumerator TypeSentence(string sentence)
{
    isTyping = true;
    dialogueText.text = "";
    foreach (char letter in sentence.ToCharArray())
    {
        if (!isTyping) // Si on a interrompu l'animation, afficher directement la phrase complète
        {
            dialogueText.text = sentence;
            break;
        }

        dialogueText.text += letter;
        yield return new WaitForSeconds(typingSpeed);
    }
    isTyping = false;
}

void Update()
{
    if (Input.GetKeyDown(KeyCode.E))
    {
        if (isTyping)
        {
            isTyping = false; // Arrête l'animation et affiche tout d'un coup
        }
        else
        {
            DisplayNextSentence();
        }
    }
}

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}
