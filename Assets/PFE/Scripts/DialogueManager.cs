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

     // Ces deux booléens gèrent l'affichage progressif
    // private bool isTyping = false;
    private bool sentenceFinished = false;

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
    string sentence = "";

    if (!isPlayerTurn && npcSentences.Count > 0)
    {
        sentence = npcSentences.Dequeue();
        isPlayerTurn = true; // ✅ On change de tour avant d'afficher
    }
    else if (isPlayerTurn && playerSentences.Count > 0)
    {
        sentence = playerSentences.Dequeue();
        isPlayerTurn = false; // ✅ On change de tour avant d'afficher
    }
    else
    {
        EndDialogue();
        return;
    }

    Debug.Log("Affichage : " + sentence);
    StopAllCoroutines();
    StartCoroutine(TypeSentence(sentence));
}

private bool isTyping = false;

IEnumerator TypeSentence(string sentence)
{
    isTyping = true;
     sentenceFinished = false;
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
    sentenceFinished = true;
}
void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!sentenceFinished)
            {
                isTyping = false;
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
