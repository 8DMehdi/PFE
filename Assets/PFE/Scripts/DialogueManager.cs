using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject dialoguePanel;
    public Text nameText;
    public Text dialogueText;
    public float typingSpeed = 0.05f;

    private Queue<string> npcSentences;
    private Queue<string> playerSentences;
    private bool isPlayerTurn = false;
    private bool sentenceFinished = false;

    private AudioSource voiceSource;
    private DialogueData currentDialogue;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        npcSentences = new Queue<string>();
        playerSentences = new Queue<string>();
        dialoguePanel.SetActive(false);

        // Ajoute un AudioSource si inexistant
        voiceSource = GetComponent<AudioSource>();
        if (voiceSource == null)
        {
            voiceSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void StartDialogue(DialogueData dialogue)
    {
        if (dialogue == null || dialogue.npcSentences.Count == 0)
        {
            Debug.LogError("Le dialogue est vide ou non assigné !");
            return;
        }

        currentDialogue = dialogue;
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
            isPlayerTurn = true;
        }
        else if (isPlayerTurn && playerSentences.Count > 0)
        {
            sentence = playerSentences.Dequeue();
            isPlayerTurn = false;
        }
        else
        {
            EndDialogue();
            return;
        }

        // Joue le son AVANT d'afficher la phrase
        PlayNPCVoice();

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
            if (!isTyping)
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!sentenceFinished)
            {
                isTyping = false;
            }
            else
            {
                DisplayNextSentence(); // Le son est joué ici
            }
        }
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    private void PlayNPCVoice()
    {
        if (voiceSource != null && currentDialogue != null && currentDialogue.npcVoice != null)
        {
            voiceSource.Stop(); // Arrête le son précédent pour éviter des chevauchements
            voiceSource.clip = currentDialogue.npcVoice;
            voiceSource.Play();
        }
    }
}