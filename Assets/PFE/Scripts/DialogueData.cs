using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue System/Dialogue")]
public class DialogueData : ScriptableObject
{
    public string npcName;

    [TextArea(3, 5)]
    public List<string> npcSentences;

    [TextArea(3, 5)]
    public List<string> playerSentences;
}
